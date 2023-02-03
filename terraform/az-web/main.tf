#--------------------------------------------------------------
#   Gathering data
#--------------------------------------------------------------
data azurerm_resource_group this {
  name = var.rg_name
}
data azurerm_mssql_database eshopweb_catalog {
  name      = lower("sqldb-cpchem-${var.team_name}-${var.iterator}-catalog")
  server_id = var.mssql_server_id
}
data azurerm_mssql_database eshopweb_identity {
  name      = lower("sqldb-cpchem-${var.team_name}-${var.iterator}-identity")
  server_id = var.mssql_server_id
}
data azurerm_key_vault_secret sql_admin_pwd {
  name         = reverse(split("/", var.mssql_server_id))[0]
  key_vault_id = var.key_vault_id
}
data azurerm_application_insights this {
  resource_group_name = split("/", var.application_insights_id)[4]
  name                = split("/", var.application_insights_id)[8]
}

#--------------------------------------------------------------
#   Transforming inputs
#--------------------------------------------------------------
locals {
  db_catalog_id = data.azurerm_mssql_database.eshopweb_catalog.id
  db_catalog_connection_string = "Server=tcp:${reverse(split("/", local.db_catalog_id))[2]}.database.windows.net,1433;Initial Catalog=${reverse(split("/", local.db_catalog_id))[0]};Persist Security Info=False;User ID=sql-admin;Password=${data.azurerm_key_vault_secret.sql_admin_pwd.value};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

  db_identity_id = data.azurerm_mssql_database.eshopweb_identity.id
  db_identity_connection_string = "Server=tcp:${reverse(split("/", local.db_identity_id))[2]}.database.windows.net,1433;Initial Catalog=${reverse(split("/", local.db_identity_id))[0]};Persist Security Info=False;User ID=sql-admin;Password=${data.azurerm_key_vault_secret.sql_admin_pwd.value};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

  tags = merge(
    data.azurerm_resource_group.this.tags,
    var.additional_tags
  )
}


#--------------------------------------------------------------
#   App Service Plan + Windows
#--------------------------------------------------------------
resource azurerm_service_plan this {
  name                = "appsvcplan-cpchem-${var.team_name}-${var.iterator}"
  resource_group_name = data.azurerm_resource_group.this.name
  location            = data.azurerm_resource_group.this.location
  sku_name            = "B1" # "P1v2"
  os_type             = "Windows"

  tags = local.tags
}

resource azurerm_windows_web_app this {
  name                = "appsvcwin-cpchem-${var.team_name}-${var.iterator}"
  resource_group_name = data.azurerm_resource_group.this.name
  location            = data.azurerm_resource_group.this.location
  service_plan_id     = azurerm_service_plan.this.id

  site_config {}

  app_settings = {
    "ASPNETCORE_ENVIRONMENT" = "Development",
    "APPINSIGHTS_INSTRUMENTATIONKEY"                  = data.azurerm_application_insights.this.instrumentation_key,
    "APPINSIGHTS_PROFILERFEATURE_VERSION"             = "1.0.0",
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"             = "1.0.0",
    "APPLICATIONINSIGHTS_CONNECTION_STRING"           = data.azurerm_application_insights.this.connection_string,
    "ApplicationInsightsAgent_EXTENSION_VERSION"      = "~2",
    "DiagnosticServices_EXTENSION_VERSION"            = "~3",
    "InstrumentationEngine_EXTENSION_VERSION"         = "disabled",
    "SnapshotDebugger_EXTENSION_VERSION"              = "disabled",
    "XDT_MicrosoftApplicationInsights_BaseExtensions" = "disabled",
    "XDT_MicrosoftApplicationInsights_Java"           = "1",
    "XDT_MicrosoftApplicationInsights_Mode"           = "recommended",
    "XDT_MicrosoftApplicationInsights_NodeJS"         = "1",
    "XDT_MicrosoftApplicationInsights_PreemptSdk"     = "disabled"
  }

  connection_string {
    name  = "CatalogConnection"
    type  = "SQLServer"
    value = local.db_catalog_connection_string
  }
  connection_string {
    name  = "IdentityConnection"
    type  = "SQLServer"
    value = local.db_identity_connection_string
  }

sticky_settings {
  app_setting_names       = [
    "APPINSIGHTS_INSTRUMENTATIONKEY",
    "APPLICATIONINSIGHTS_CONNECTION_STRING",
    "APPINSIGHTS_PROFILERFEATURE_VERSION",
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION",
    "ApplicationInsightsAgent_EXTENSION_VERSION",
    "XDT_MicrosoftApplicationInsights_BaseExtensions",
    "DiagnosticServices_EXTENSION_VERSION",
    "InstrumentationEngine_EXTENSION_VERSION",
    "SnapshotDebugger_EXTENSION_VERSION",
    "XDT_MicrosoftApplicationInsights_Mode",
    "XDT_MicrosoftApplicationInsights_PreemptSdk",
    "APPLICATIONINSIGHTS_CONFIGURATION_CONTENT",
    "XDT_MicrosoftApplicationInsightsJava",
    "XDT_MicrosoftApplicationInsights_NodeJS",
  ]
}

  tags = local.tags
}
#*/
