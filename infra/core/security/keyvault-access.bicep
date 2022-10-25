param environmentName string
param location string = resourceGroup().location

param keyVaultName string = ''
param permissions object = { secrets: [ 'get', 'list' ] }
param principalId string

var abbrs = loadJsonContent('../../abbreviations.json')
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))

resource keyVaultAccessPolicies 'Microsoft.KeyVault/vaults/accessPolicies@2022-07-01' = {
  parent: keyVault
  name: 'add'
  properties: {
    accessPolicies: [ {
        objectId: principalId
        tenantId: subscription().tenantId
        permissions: permissions
      } ]
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' existing = {
  name: !empty(keyVaultName) ? keyVaultName : '${abbrs.keyVaultVaults}${resourceToken}'
}
