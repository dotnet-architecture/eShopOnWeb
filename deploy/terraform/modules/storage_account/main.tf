terraform {
  required_version = ">= 1.3"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }
}

resource "azurerm_storage_account" "storage_account" {
  name                = var.name
  resource_group_name = var.resource_group_name

  location                 = var.location
  account_kind             = var.account_kind
  account_tier             = var.account_tier
  account_replication_type = var.replication_type
  is_hns_enabled           = var.is_hns_enabled
  tags                     = var.tags

  network_rules {
    default_action             = (length(var.ip_rules) + length(var.virtual_network_subnet_ids)) > 0 ? "Deny" : var.default_action
    ip_rules                   = var.ip_rules
    virtual_network_subnet_ids = var.virtual_network_subnet_ids
  }

  identity {
    type = "SystemAssigned"
  }

  lifecycle {
    ignore_changes = [
        tags
    ]
  }
}