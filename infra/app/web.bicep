param name string
param location string = resourceGroup().location
param tags object = {}
param serviceName string = 'web'
param appCommandLine string = 'pm2 serve /home/site/wwwroot --no-daemon --spa'
param applicationInsightsName string = ''
param appServicePlanId string
param appSettings object = {}

module web '../core/host/appservice.bicep' = {
  name: '${name}-deployment'
  params: {
    name: name
    location: location
    appServicePlanId: appServicePlanId
    runtimeName: 'dotnetcore'
    runtimeVersion: '6.0'
    tags: union(tags, { 'azd-service-name': serviceName })
    scmDoBuildDuringDeployment: false
  }
}

output REACT_APP_WEB_BASE_URL string = web.outputs.uri
