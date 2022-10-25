param environmentName string
param location string = resourceGroup().location

param databaseName string = 'CatalogDB'
param keyVaultName string

@secure()
param sqlAdminPassword string
@secure()
param appUserPassword string

module sqlServer1 '../core/database/sqlserver1.bicep' = {
  name: 'sqlServer1'
  params: {
    environmentName: environmentName
    location: location
    dbName: databaseName
    keyVaultName: keyVaultName
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
  }
}

output sqlConnectionStringKey string = sqlServer1.outputs.sqlConnectionStringKey
output sqlDatabase1Name string = databaseName
