targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the the environment which is used to generate a short unique hash used in all resources.')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

// Optional parameters to override the default azd resource naming conventions. Update the main.parameters.json file to provide values. e.g.,:
// "resourceGroupName": {
//      "value": "myGroupName"
// }
param resourceGroupName string = ''
param webServiceName string = ''
param catalogDatabaseName string = 'catalogDatabase'
param catalogDatabaseServerName string = ''
param identityDatabaseName string = 'identityDatabase'
param identityDatabaseServerName string = ''
param appServicePlanName string = ''
param keyVaultName string = ''

@description('Id of the user or app to assign application roles')
param principalId string = ''

@secure()
@description('SQL Server administrator password')
param sqlAdminPassword string

@secure()
@description('Application user password')
param appUserPassword string

var abbrs = loadJsonContent('./abbreviations.json')
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))
var tags = { 'azd-env-name': environmentName }

// Organize resources in a resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: !empty(resourceGroupName) ? resourceGroupName : '${abbrs.resourcesResourceGroups}${environmentName}'
  location: location
  tags: tags
}

// The application frontend
module web './core/host/appservice.bicep' = {
  name: 'web'
  scope: rg
  params: {
    name: !empty(webServiceName) ? webServiceName : '${abbrs.webSitesAppService}web-${resourceToken}'
    location: location
    appServicePlanId: appServicePlan.outputs.id
    runtimeName: 'dotnetcore'
    runtimeVersion: '6.0'
    tags: union(tags, { 'azd-service-name': 'web' })
    appSettings: {
      CATALOG_CONNECTION_STRING_VALUE: '${catalogDb.outputs.connectionString}; Password=${appUserPassword}'
      IDENTITY_CONNECTION_STRING_VALUE: '${identityDb.outputs.connectionString}; Password=${appUserPassword}'
    }
  }
}

// The application database: Catalog
module catalogDb './core/database/sqlserver/sqlserver.bicep' = {
  name: 'sql-catalog'
  scope: rg
  params: {
    name: !empty(catalogDatabaseServerName) ? catalogDatabaseServerName : '${abbrs.sqlServers}catalog-${resourceToken}'
    databaseName: catalogDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
    connectionStringKey: 'AZURE-SQL-CATALOG-CONNECTION-STRING'
  }
}

// The application database: Identity
module identityDb './core/database/sqlserver/sqlserver.bicep' = {
  name: 'sql-identity'
  scope: rg
  params: {
    name: !empty(identityDatabaseServerName) ? identityDatabaseServerName : '${abbrs.sqlServers}identity-${resourceToken}'
    databaseName: identityDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
    connectionStringKey: 'AZURE-SQL-IDENTITY-CONNECTION-STRING'
  }
}

// Store secrets in a keyvault
module keyVault './core/security/keyvault.bicep' = {
  name: 'keyvault'
  scope: rg
  params: {
    name: !empty(keyVaultName) ? keyVaultName : '${abbrs.keyVaultVaults}${resourceToken}'
    location: location
    tags: tags
    principalId: principalId
  }
}

// Create an App Service Plan to group applications under the same payment plan and SKU
module appServicePlan './core/host/appserviceplan.bicep' = {
  name: 'appserviceplan'
  scope: rg
  params: {
    name: !empty(appServicePlanName) ? appServicePlanName : '${abbrs.webServerFarms}${resourceToken}'
    location: location
    tags: tags
    sku: {
      name: 'B1'
    }
  }
}

output AZURE_LOCATION string = location
output AZURE_TENANT_ID string = tenant().tenantId
