param environmentName string
param location string = resourceGroup().location

param databaseName string = 'IdentityDB'
param keyVaultName string

@secure()
param sqlAdminPassword string
@secure()
param appUserPassword string

module sqlServer2 '../core/database/sqlserver2.bicep' = {
  name: 'sqlServer2'
  params: {
    environmentName: environmentName
    location: location
    dbName: databaseName
    keyVaultName: keyVaultName
    sqlAdminPassword: sqlAdminPassword
    appUserPassword: appUserPassword
  }
}

output sqlConnectionStringKey string = sqlServer2.outputs.sqlConnectionStringKey
output sqlDatabase2Name string = databaseName
