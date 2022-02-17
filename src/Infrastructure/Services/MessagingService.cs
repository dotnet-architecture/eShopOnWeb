using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using BlazorShared;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class MessagingService : IMessagingService
{
    private const string _queueName = "ordersqueue";
    private readonly string _serviceBusUrl;

    public MessagingService(IOptions<BaseUrlConfiguration> options)
    {
        _serviceBusUrl = options.Value.ServiceBusBase;
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellation = default)
    {
        await using var client = new ServiceBusClient(_serviceBusUrl);
        ServiceBusSender sender = client.CreateSender(_queueName);

        var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var serializedMessage = JsonSerializer.Serialize(message, serializerOptions);

        await sender.SendMessageAsync(new ServiceBusMessage(serializedMessage), cancellation);
    }
}
