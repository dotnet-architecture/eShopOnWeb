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


module "container_apps" {
  depends_on = [
    module.mssql
  ]
  source                           = "./modules/container_apps"
  managed_environment_name         = "${var.resource_prefix != "" ? var.resource_prefix : random_string.resource_prefix.result}${var.managed_environment_name}"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  infrastructure_subnet_id         = module.virtual_network.subnet_ids[var.aca_subnet_name] 
  instrumentation_key              = module.application_insights.instrumentation_key
  workspace_id                     = module.log_analytics_workspace.id
  dapr_components                  = [{
      name            = var.dapr_name
      component_type  = var.dapr_component_type
      version         = var.dapr_version
      ignore_errors   = var.dapr_ignore_errors
      init_timeout    = var.dapr_init_timeout
      secret          = [
        {
          name        = "storageaccountkey"
          value       = module.storage_account.primary_access_key
        }
      ]
      metadata: [
        {
          name        = "accountName"
          value       = module.storage_account.name
        },
        {
          name        = "containerName"
          value       = var.container_name
        },
        {
          name        = "accountKey"
          secret_name = "storageaccountkey"
        }
      ]
      scopes          = var.dapr_scopes
      }]
  container_apps                   = [{
      name                = "eshop-web"
      revision_mode       = "Single"
      ingress             = {
          external_enabled    = true
          target_port         = 80
          transport           = "http"
          traffic_weight      = [{
              label               = "blue"
              latest_revision     = true
              revision_suffix     = "blue"
              percentage          = 100
          }]
      }
      dapr                           = {
          app_id               = "eshop-web"
          app_port             = 80
          app_protocol         = "http"
      }
      template              = {
        containers            = [{
            name                 = "web"
            image                = var.app_image
            cpu                  = 0.5
            memory               = "1Gi"
            env                  = [{
                name                 = "APPLICATIONINSIGHTS_CONNECTION_STRING"
                secret_name          = "insights-connection"
            },
            {
                name                 = "ASPNETCORE_URLS"
                value                = "http://+:80"
            },
            {
                name                 = "ConnectionStrings__CatalogConnection"
                secret_name          = "catalog-connection"
            },
            {
                name                 = "ConnectionStrings__IdentityConnection"
                secret_name          = "identity-connection"
            },
            {
                name                 = "Testing__LoadTestsAuthenticationEnabled"
                value                = true
            }]
        }]
        min_replicas                 = 1
        max_replicas                 = 1
      }
      secrets = [
        {
          name = "insights-connection"
          value = module.application_insights.connection_string
        },
        {
          name = "insights-instrumentationkey"
          value = module.application_insights.instrumentation_key
        },
        {
          name = "catalog-connection"
          value = module.mssql.conection_strings.Catalog
        },
        {
          name = "identity-connection"
          value = module.mssql.conection_strings.Identity
        }
      ]
    }]
}