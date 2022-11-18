param name string
param location string = resourceGroup().location
param tags object = {}

param databaseName string = 'CatalogDB'
param keyVaultName string

@secure()
param sqlAdminPassword string
@secure()
param appUserPassword string

// Because databaseName is optional in main.bicep, we make sure the database name is set here.
var defaultDatabaseName = 'Todo'
var actualDatabaseName = !empty(databaseName) ? databaseName : defaultDatabaseName

module sqlServer1 '../core/database/sqlserver/sqlserver-catalog.bicep' = {
  name: 'sqlServer01'
  params: {
    name: name
    location: location
    tags: tags
    databaseName: actualDatabaseName
    keyVaultName: keyVaultName
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
  }
}

output sqlCatalogConnectionStringKey string = sqlServer1.outputs.connectionStringKey
output sqlCatalogDatabase1Name string = sqlServer1.outputs.databaseName
