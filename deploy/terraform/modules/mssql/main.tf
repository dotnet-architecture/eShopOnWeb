terraform {
  required_version = ">= 1.3"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }
}

resource "random_password" "sql_password" {
  length = 16
  special = true
  override_special = "!#~$%&,."
}

resource "azurerm_key_vault_secret" "kv-sql-secret" {
  key_vault_id = var.key_vault_id
  name         = "${var.name}-password"
  value        = random_password.sql_password.result
}

resource "azurerm_mssql_server" "server" {
  name                         = var.name
  resource_group_name          = var.resource_group_name
  location                     = var.location
  version                      = "12.0"
  administrator_login          = "${var.name}-admin"
  administrator_login_password = azurerm_key_vault_secret.kv-sql-secret.value
}

resource "azurerm_mssql_firewall_rule" "firewall_rule" {
  name             = "Allow access to Azure services"
  server_id        = azurerm_mssql_server.server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "db" {
  for_each       = var.databases
  name           = each.key
  server_id      = azurerm_mssql_server.server.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  
  min_capacity   = 0.5
  max_size_gb    = 4
  read_scale     = false
  sku_name       = "GP_S_Gen5_1"
  zone_redundant = false
  auto_pause_delay_in_minutes = 60
  

  tags = var.tags
}