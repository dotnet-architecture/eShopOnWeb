param name string
param location string = resourceGroup().location
param tags object = {}

param databaseName string = 'IdentityDB'
param keyVaultName string

@secure()
param sqlAdminPassword string
@secure()
param appUserPassword string

// Because databaseName is optional in main.bicep, we make sure the database name is set here.
var defaultDatabaseName = 'Todo'
var actualDatabaseName = !empty(databaseName) ? databaseName : defaultDatabaseName

module sqlServer2 '../core/database/sqlserver/sqlserver-identity.bicep' = {
  name: 'sqlServer02'
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

output sqlCatalogConnectionStringKey string = sqlServer2.outputs.connectionStringKey
output sqlCatalogDatabase1Name string = sqlServer2.outputs.databaseName
