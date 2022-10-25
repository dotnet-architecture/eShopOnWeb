param environmentName string
param location string = resourceGroup().location

param sku object = {
  name: 'Y1'
  tier: 'Dynamic'
  size: 'Y1'
  family: 'Y'
}

module appServicePlanFunctions 'appserviceplan.bicep' = {
  name: 'appserviceplan-functions'
  params: {
    environmentName: environmentName
    location: location
    sku: sku
    kind: 'functionapp'
  }
}

output appServicePlanId string = appServicePlanFunctions.outputs.appServicePlanId
