using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.eShopWeb.Web.Interfaces;
namespace Microsoft.eShopWeb.Web.Services;

public class PublishEventService : IPublishEventService
{
    private readonly TelemetryClient _telemetryClient;

    public PublishEventService(TelemetryClient telemetryClient)
    {
        _telemetryClient = telemetryClient;
    }


    public void PublishEvent(EventType eventType, IDictionary<string, string> properties)
    {
        try
        {
            _telemetryClient.TrackEvent(eventType.ToString(), properties);
        }
        catch
        {
            //ignore;
        }
    }
}
