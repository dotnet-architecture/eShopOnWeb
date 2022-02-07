using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.Interfaces;

public enum EventType
{
    PageOpenings,
    AddToCart,
    Checkout
}


public interface IPublishEventService
{
    void PublishEvent(EventType eventType, IDictionary<string, string> properties);
}
