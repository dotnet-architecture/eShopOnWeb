param additionalConfigProperties object
param appServiceName string
param configName string
param currentConfigProperties object

resource siteConfigUnion 'Microsoft.Web/sites/config@2022-03-01' = {
  name: '${appServiceName}/${configName}'
  properties: union(currentConfigProperties, additionalConfigProperties)
}
