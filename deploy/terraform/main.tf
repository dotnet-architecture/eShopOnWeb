terraform {
  required_version = ">= 1.3"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

data "azurerm_client_config" "current" {
}

resource "random_string" "resource_prefix" {
  length  = 6
  special = false
  upper   = false
  numeric  = false
}

resource "azurerm_resource_group" "rg" {
  name     = "${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.resource_group_name}"
  location = var.location
  tags     = var.tags
}

resource "azurerm_key_vault" "kv" {
  name                = "${var.resource_prefix}-kv"
  resource_group_name = azurerm_resource_group.rg.name
  tenant_id           = data.azurerm_client_config.current.tenant_id
  location            = azurerm_resource_group.rg.location
  sku_name            = var.kv_sku_name
  access_policy {
    tenant_id         = data.azurerm_client_config.current.tenant_id
    object_id         = data.azurerm_client_config.current.object_id

    secret_permissions = [
      "Set",
      "Get",
      "Delete",
      "Purge",
      "Recover",
      "List"
    ]
  }
}

module "log_analytics_workspace" {
  source                           = "./modules/log_analytics"
  name                             = "${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.log_analytics_workspace_name}"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
}

module "application_insights" {
  source                           = "./modules/application_insights"
  name                             = "${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.application_insights_name}"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  application_type                 = var.application_insights_application_type
  workspace_id                     = module.log_analytics_workspace.id
}

module "virtual_network" {
  source                           = "./modules/virtual_network"
  resource_group_name              = azurerm_resource_group.rg.name
  vnet_name                        = "${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.vnet_name}"
  location                         = var.location
  address_space                    = var.vnet_address_space
  tags                             = var.tags
  log_analytics_workspace_id       = module.log_analytics_workspace.id
  log_analytics_retention_days     = var.log_analytics_retention_days

  subnets = [
    {
      name : var.aca_subnet_name
      address_prefixes : var.aca_subnet_address_prefix
      private_endpoint_network_policies_enabled : true
      private_link_service_network_policies_enabled : false
    },
    {
      name : var.private_endpoint_subnet_name
      address_prefixes : var.private_endpoint_subnet_address_prefix
      private_endpoint_network_policies_enabled : true
      private_link_service_network_policies_enabled : false
    }
  ]
}
