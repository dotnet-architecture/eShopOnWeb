module "mssql" {
  source                           = "./modules/mssql"
  name                             = "${var.resource_prefix}-sql"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  key_vault_id                     = azurerm_key_vault.kv.id
  databases = {
    "Catalog" = {}
    "Identity" = {}
  }
}
