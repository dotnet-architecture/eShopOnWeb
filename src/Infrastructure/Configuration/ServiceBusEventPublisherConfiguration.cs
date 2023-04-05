using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Configuration;
public class ServiceBusEventPublisherConfiguration
{
    public Dictionary<string, string> TopicOrQueueNames { get; set; } = new Dictionary<string, string>();

    public string? GetTopicOrQueueName<T>()
    {
        string eventType = typeof(T).Name;
        if (!TopicOrQueueNames.TryGetValue(eventType, out var topicOrQueueName))
        {
            throw new InvalidOperationException($"No topic or queue name has been configured for event type '{eventType}'");
        }

        return topicOrQueueName;
    }
}
