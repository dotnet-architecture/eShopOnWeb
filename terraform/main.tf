terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }
  required_version = ">= 0.14.9"
}

provider "azurerm" {
  features {}
}

locals {
  name = "squirr3l-module3-eshop"

  #run the commands below to get it (for macos / linux):
  #dotnet publish ./src/PublicApi/PublicApi.csproj -c Release -o eshop.zip
  #zip -r eshop.zip ./src/PublicApi/bin/Release/net7.0/publish
  file_path = "../eshop.zip"
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

  #throw an error if file doesn't exist
  zip_deploy_file = fileexists(local.file_path) ? local.file_path : [][0]
  
  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = 1
  }
  
  site_config {
    minimum_tls_version = "1.2"

    application_stack {
      dotnet_version = "7.0"
    }
  }
}

resource "azurerm_linux_web_app_slot" "example" {
  name           = "web-app-slot-${local.name}"
  app_service_id = azurerm_linux_web_app.webapp.id
  https_only     = true

  #throw an error if file doesn't exist
  zip_deploy_file = fileexists(local.file_path) ? local.file_path : [][0]

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = 1
  }

  site_config {
    minimum_tls_version = "1.2"

    application_stack {
      dotnet_version = "7.0"
    }
  }
}
