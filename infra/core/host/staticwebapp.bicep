param environmentName string
param location string = resourceGroup().location

param serviceName string
param sku object = {
  name: 'Free'
  tier: 'Free'
}

var abbrs = loadJsonContent('../../abbreviations.json')
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))
var tags = { 'azd-env-name': environmentName }

resource web 'Microsoft.Web/staticSites@2022-03-01' = {
  name: '${abbrs.webStaticSites}${serviceName}-${resourceToken}'
  location: location
  tags: union(tags, { 'azd-service-name': serviceName })
  sku: sku
  properties: {
    provider: 'Custom'
  }
}

output name string = web.name
output uri string = 'https://${web.properties.defaultHostname}'
