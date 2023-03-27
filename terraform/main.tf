terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.48.0"
    }
  }
}

provider "azurerm" {
  features {}
}

locals {
  name = "squirr3l-module3-eshop"

  #run the commands below to get it (for macos / linux):
  #dotnet publish ./src/PublicApi/PublicApi.csproj -c Release
  #zip -r public_api.zip ./src/PublicApi/bin/Release/net7.0/publish
  public_api_file_path = "../public_api.zip"

  #dotnet publish ./src/Web/Web.csproj -c Release
  #zip -r web.zip ./src/Web/bin/Release/net7.0/publish
  web_file_path = "../web.zip"

  #throw an error if file doesn't exist
  public_api_zip_deploy_file = fileexists(local.public_api_file_path) ? local.public_api_file_path : [][0]
  web_zip_deploy_file        = fileexists(local.web_file_path) ? local.web_file_path : [][0]
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
}

resource "azurerm_linux_web_app" "main" {
  name                = "webapp-${local.name}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.appserviceplan.id
  https_only          = true

  zip_deploy_file = local.web_zip_deploy_file
  
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

resource "azurerm_linux_web_app_slot" "main" {
  name           = "develop"
  app_service_id = azurerm_linux_web_app.main.id
  https_only     = true

  zip_deploy_file = local.web_zip_deploy_file

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