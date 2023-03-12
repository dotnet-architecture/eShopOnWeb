terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0.0"
    }
  }
  required_version = ">= 0.14.9"
}

provider "azurerm" {
  features {}
}

locals {
  name = "squirr3l-module3-eshop"
}

resource "azurerm_resource_group" "rg" {
  name     = "resource-group-${local.name}"
  location = "westeurope"
}

# Create the Linux App Service Plan
resource "azurerm_service_plan" "appserviceplan" {
  name                   = "service-plan-${local.name}"
  location               = azurerm_resource_group.rg.location
  resource_group_name    = azurerm_resource_group.rg.name
  os_type                = "Linux"
  sku_name               = "B1"
  worker_count           = 2
  
  connection {
    zone_balancing_enabled = true
  }
}

# Create the web app, pass in the App Service Plan ID
resource "azurerm_linux_web_app" "webapp" {
  name                  = "webapp-${local.name}"
  location              = azurerm_resource_group.rg.location
  resource_group_name   = azurerm_resource_group.rg.name
  service_plan_id       = azurerm_service_plan.appserviceplan.id
  https_only            = true
  site_config {
    minimum_tls_version = "1.2"
  }
}

#  Deploy code from a public GitHub repo
resource "azurerm_app_service_source_control" "sourcecontrol" {
  app_id                 = azurerm_linux_web_app.webapp.id
  repo_url               = "https://github.com/evilsquirr3l/eShopOnWeb.git"
  branch                 = "main"
  use_manual_integration = true
  use_mercurial          = false
}