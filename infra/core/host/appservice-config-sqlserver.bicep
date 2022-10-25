param appServiceName string
param sqlConnectionStringKey string

module appServiceConfigSqlServerSettings 'appservice-config-union.bicep' = {
  name: '${appServiceName}-appservice-config-sqlserver-settings'
  params: {
    appServiceName: appServiceName
    configName: 'appsettings'
    currentConfigProperties: list(resourceId('Microsoft.Web/sites/config', appServiceName, 'appsettings'), '2022-03-01').properties
    additionalConfigProperties: {
      AZURE_SQL_CONNECTION_STRING_KEY: sqlConnectionStringKey
    }
  }
}
