param environmentName string
param location string = resourceGroup().location
param principalId string = ''

@secure()
param sqlAdminPassword string

@secure()
param appUserPassword string

// The application frontend
module web './app/web.bicep' = {
  name: 'web'
  params: {
    environmentName: environmentName
    location: location
    appServicePlanId: appServicePlan.outputs.appServicePlanId
  }
}

// The application database: Catalog
module sqlServer1 './app/dbCatalog.bicep' = {
  name: 'sqlCatalog'
  params: {
    environmentName: environmentName
    location: location
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.keyVaultName
  }
}

// The application database: Identity
module sqlServer2 './app/dbIdentity.bicep' = {
  name: 'sqlIdentity'
  params: {
    environmentName: environmentName
    location: location
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.keyVaultName
  }
}

// Configure web to use sqlCatalog
module apiSqlServerConfig1 './core/host/appservice-config-sqlserver.bicep' = {
  name: 'web-sqlserver-config-1'
  params: {
    appServiceName: web.outputs.WEB_NAME
    sqlConnectionStringKey: sqlServer1.outputs.sqlConnectionStringKey
  }
}

// Configure web to use sqlIdentity
module apiSqlServerConfig2 './core/host/appservice-config-sqlserver.bicep' = {
  name: 'web-sqlserver-config-2'
  params: {
    appServiceName: web.outputs.WEB_NAME
    sqlConnectionStringKey: sqlServer2.outputs.sqlConnectionStringKey
  }
}

// Store secrets in a keyvault
module keyVault './core/security/keyvault.bicep' = {
  name: 'keyvault'
  params: {
    environmentName: environmentName
    location: location
    principalId: principalId
  }
}

// Create an App Service Plan to group applications under the same payment plan and SKU
module appServicePlan './core/host/appserviceplan-sites.bicep' = {
  name: 'appserviceplan'
  params: {
    environmentName: environmentName
    location: location
  }
}

output WEB_URI string = web.outputs.WEB_URI
