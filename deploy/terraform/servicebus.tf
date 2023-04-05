module "servicebus" {
  source                           = "./modules/servicebus"
  name                             = "${var.resource_prefix}-sbus"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  topics = {
    "order-payment-succeeded" = {
        subscriptions = {
            "ReservedQuantityUpdater" = { max_delivery_count = 3 }
        }
    }
  }
}
