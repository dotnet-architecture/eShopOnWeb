param environmentName string
param location string = resourceGroup().location
param appServicePlanId string

param serviceName string = 'web'

module web '../core/host/appservice-dotnet.bicep' = {
  name: '${serviceName}-appservice-dotnet-module'
  params: {
    environmentName: environmentName
    location: location
    appServicePlanId: appServicePlanId
    serviceName: serviceName
  }
}

output WEB_NAME string = web.outputs.name
output WEB_URI string = web.outputs.uri