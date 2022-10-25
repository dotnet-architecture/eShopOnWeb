param appServiceName string

resource siteConfigLogs 'Microsoft.Web/sites/config@2022-03-01' = {
  name: '${appServiceName}/logs'
  properties: {
    applicationLogs: { fileSystem: { level: 'Verbose' } }
    detailedErrorMessages: { enabled: true }
    failedRequestsTracing: { enabled: true }
    httpLogs: { fileSystem: { enabled: true, retentionInDays: 1, retentionInMb: 35 } }
  }
}
