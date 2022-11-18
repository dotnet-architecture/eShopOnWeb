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
param sqlServer1Name string = 'sqlServer-catalog-01'
param sqlServer2Name string = 'sqlServer-identity-01'
param sqlDatabaseName string = ''
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
module web './app/web.bicep' = {
  name: 'web'
  scope: rg
  params: {
    name: !empty(webServiceName) ? webServiceName : '${abbrs.webSitesAppService}web-${resourceToken}'
    location: location
    tags: tags
    appServicePlanId: appServicePlan.outputs.id
  }
}

// The application database: Catalog
module sqlServer1 './app/catalog-db.bicep' = {
  name: 'sql-catalog'
  scope: rg
  params: {
    name: !empty(sqlServer1Name) ? sqlServer1Name : '${abbrs.sqlServers}${resourceToken}'
    databaseName: sqlDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
  }
}

// The application database: Identity
module sqlServer2 './app/identity-db.bicep' = {
  name: 'sql-identity'
  scope: rg
  params: {
    name: !empty(sqlServer2Name) ? sqlServer2Name : '${abbrs.sqlServers}${resourceToken}'
    databaseName: sqlDatabaseName
    location: location
    tags: tags
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
    keyVaultName: keyVault.outputs.name
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
