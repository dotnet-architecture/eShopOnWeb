param name string
param location string = resourceGroup().location
param tags object = {}

param allowBlobPublicAccess bool = false
param containers array = []
param kind string = 'StorageV2'
param minimumTlsVersion string = 'TLS1_2'
param sku object = { name: 'Standard_LRS' }

resource storage 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: name
  location: location
  tags: tags
  kind: kind
  sku: sku
  properties: {
    minimumTlsVersion: minimumTlsVersion
    allowBlobPublicAccess: allowBlobPublicAccess
    networkAcls: {
      bypass: 'AzureServices'
      defaultAction: 'Allow'
    }
  }

  resource blobServices 'blobServices' = if (!empty(containers)) {
    name: 'default'
    resource container 'containers' = [for container in containers: {
      name: container.name
      properties: {
        publicAccess: contains(container, 'publicAccess') ? container.publicAccess : 'None'
      }
    }]
  }
}

output name string = storage.name
output primaryEndpoints object = storage.properties.primaryEndpoints
