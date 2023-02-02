# Base resources for an Azure Application Landing Zone

#--------------------------------------------------------------
#  Application Landing Zone
#--------------------------------------------------------------
#   Current configuration context
data azurerm_client_config this {}

#   Resource Group
resource azurerm_resource_group this {
  name     = "rg-cpchem-${var.team_name}"
  location = var.location

  tags = var.base_tags
}

#   Storage Account
resource azurerm_storage_account this {
  name                     = "stcpchem${replace(var.team_name, "-", "")}"
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = azurerm_resource_group.this.tags
}

#   Key vault
resource azurerm_key_vault this {
  name                        = "kv-cpchem-${var.team_name}-${var.iterator}"
  location                    = azurerm_resource_group.this.location
  resource_group_name         = azurerm_resource_group.this.name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.this.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = false
  enable_rbac_authorization   = true

  sku_name = "standard"

  tags = azurerm_resource_group.this.tags
}
resource azurerm_role_assignment kv-admin {
  principal_id          = data.azurerm_client_config.this.client_id
  role_definition_name  = "Key Vault Administrator"
  scope                 = azurerm_key_vault.this.id
}

#   Virtual Network
resource "azurerm_virtual_network" "this" {
  name                = lower("vnet-cpchem-${var.team_name}-${var.iterator}")
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  address_space       = var.address_space
  dns_servers         = var.dns_servers

  tags = azurerm_resource_group.this.tags
}

#   Subnets
resource "azurerm_subnet" "this" {
  for_each = var.subnets

  name                                           = each.key
  resource_group_name                            = azurerm_resource_group.this.name
  address_prefixes                               = each.value["address_prefixes"]
  service_endpoints                              = lookup(each.value, "service_endpoints", null)
  private_endpoint_network_policies_enabled      = lookup(each.value, "pe_enable", false)
  private_link_service_network_policies_enabled  = lookup(each.value, "pe_enable", false)
  virtual_network_name                           = azurerm_virtual_network.this.name

  dynamic "delegation" {
    for_each = lookup(each.value, "delegation", [])
    content {
      name = lookup(delegation.value, "name", null)
      dynamic "service_delegation" {
        for_each = lookup(delegation.value, "service_delegation", [])
        content {
          name    = lookup(service_delegation.value, "name", null)
          actions = lookup(service_delegation.value, "actions", null)
        }
      }
    }
  }

  depends_on = [azurerm_virtual_network.this]
}




#   Network Security Groups


#   Application Insights

