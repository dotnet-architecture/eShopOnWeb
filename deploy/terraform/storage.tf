module "blob_private_dns_zone" {
  source                       = "./modules/private_dns_zone"
  name                         = "privatelink.blob.core.windows.net"
  resource_group_name          = azurerm_resource_group.rg.name
  virtual_networks_to_link     = {
    (module.virtual_network.name) = {
      subscription_id = data.azurerm_client_config.current.subscription_id
      resource_group_name = azurerm_resource_group.rg.name
    }
  }
}

module "blob_private_endpoint" {
  source                         = "./modules/private_endpoint"
  name                           = "${title(module.storage_account.name)}PrivateEndpoint"
  location                       = var.location
  resource_group_name            = azurerm_resource_group.rg.name
  subnet_id                      = module.virtual_network.subnet_ids[var.private_endpoint_subnet_name]
  tags                           = var.tags
  private_connection_resource_id = module.storage_account.id
  is_manual_connection           = false
  subresource_name               = "blob"
  private_dns_zone_group_name    = "BlobPrivateDnsZoneGroup"
  private_dns_zone_group_ids     = [module.blob_private_dns_zone.id]
}

module "storage_account" {
  source                           = "./modules/storage_account"
  name                             = lower("${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.storage_account_name}")
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  account_kind                     = var.storage_account_kind
  account_tier                     = var.storage_account_tier
  replication_type                 = var.storage_account_replication_type
}
