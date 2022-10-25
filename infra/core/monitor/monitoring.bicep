param environmentName string
param location string = resourceGroup().location

module logAnalytics 'loganalytics.bicep' = {
  name: 'loganalytics'
  params: {
    environmentName: environmentName
    location: location
  }
}

module applicationInsights 'applicationinsights.bicep' = {
  name: 'applicationinsights'
  params: {
    environmentName: environmentName
    location: location
    logAnalyticsWorkspaceId: logAnalytics.outputs.logAnalyticsWorkspaceId
  }
}

output applicationInsightsConnectionString string = applicationInsights.outputs.applicationInsightsConnectionString
output applicationInsightsName string = applicationInsights.outputs.applicationInsightsName
output logAnalyticsWorkspaceId string = logAnalytics.outputs.logAnalyticsWorkspaceId
output logAnalyticsWorkspaceName string = logAnalytics.outputs.logAnalyticsWorkspaceName
