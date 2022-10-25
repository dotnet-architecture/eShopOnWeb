param environmentName string
param location string = resourceGroup().location

param sku object = { name: 'B1' }

module appServicePlanSites 'appserviceplan.bicep' = {
  name: 'appserviceplan-sites'
  params: {
    environmentName: environmentName
    location: location
    sku: sku
  }
}

output appServicePlanId string = appServicePlanSites.outputs.appServicePlanId
