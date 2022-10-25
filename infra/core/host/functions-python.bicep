param environmentName string
param location string = resourceGroup().location

param allowedOrigins array = []
param applicationInsightsName string = ''
param appServicePlanId string
param appSettings object = {}
param keyVaultName string = ''
param linuxFxVersion string = 'PYTHON|3.8'
param managedIdentity bool = !(empty(keyVaultName))
param serviceName string
param storageAccountName string

module functions 'functions.bicep' = {
  name: '${serviceName}-functions-python'
  params: {
    environmentName: environmentName
    location: location
    allowedOrigins: allowedOrigins
    applicationInsightsName: applicationInsightsName
    appServicePlanId: appServicePlanId
    appSettings: appSettings
    functionsWorkerRuntime: 'python'
    keyVaultName: keyVaultName
    linuxFxVersion: linuxFxVersion
    managedIdentity: managedIdentity
    serviceName: serviceName
    storageAccountName: storageAccountName
  }
}

output identityPrincipalId string = functions.outputs.identityPrincipalId
output name string = functions.outputs.name
output uri string = functions.outputs.uri
