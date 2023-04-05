using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Infrastructure.Services;
public class ServiceBusEventPublisher<T> : IEventPublisher<T> where T : class
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly IOptions<ServiceBusEventPublisherConfiguration> _options;

    public ServiceBusEventPublisher(ServiceBusClient serviceBusClient,IOptions<ServiceBusEventPublisherConfiguration> options)
    {
        _serviceBusClient = serviceBusClient;
        _options = options;
    }

    public Task PublishEvent(T @event, CancellationToken cancellationToken = default)
    {
        var topicOrQueueName = _options.Value.GetTopicOrQueueName<T>();

        // create the sender
        var sender = _serviceBusClient.CreateSender(topicOrQueueName);

        // create a message that we can send. UTF-8 encoding is used when providing a string.

        var messageContent = @event.ToJson();
        ServiceBusMessage message = new ServiceBusMessage(messageContent);

        // send the message
        return sender.SendMessageAsync(message);
    }
}
