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

resource "azurerm_service_plan" "appserviceplan" {
  name                = "service-plan-${local.name}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  sku_name            = "S1"
  worker_count        = 2

  connection {
    zone_balancing_enabled = true
  }
}

resource "azurerm_linux_web_app" "webapp" {
  name                = "webapp-${local.name}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.appserviceplan.id
  https_only          = true

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = 1
  }
  
  site_config {
    minimum_tls_version = "1.2"
  }
}

resource "azurerm_linux_web_app_slot" "example" {
  name           = "web-app-slot-${local.name}"
  app_service_id = azurerm_linux_web_app.webapp.id
  https_only     = true

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = 1
  }
  
  site_config {
    minimum_tls_version = "1.2"
  }
}
