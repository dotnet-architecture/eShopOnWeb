using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Text;
using System.Text.Json;
using System;
using Microsoft.Extensions.Options;
using BlazorShared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Messaging.ServiceBus;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IRepository<Basket> _basketRepository;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IAppLogger<OrderService> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _funcUrl;
    private readonly IConfiguration _configuration;
    private readonly ServiceBusSender _sender;

    public OrderService(IRepository<Basket> basketRepository,
        IRepository<CatalogItem> itemRepository,
        IRepository<Order> orderRepository,
        IUriComposer uriComposer,
        IAppLogger<OrderService> logger,
        HttpClient httpClient,
        IOptions<BaseUrlConfiguration> baseUrlConfiguration,
        IConfiguration configuration)
    {
        _orderRepository = orderRepository;
        _uriComposer = uriComposer;
        _basketRepository = basketRepository;
        _itemRepository = itemRepository;
        _logger = logger;
        _httpClient = httpClient;
        _funcUrl = baseUrlConfiguration.Value.FuncBase;
        _configuration = configuration;
        _sender = buildSender();
    }

    public async Task CreateOrderAsync(int basketId, Address shippingAddress)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await _basketRepository.GetBySpecAsync(basketSpec);

        Guard.Against.NullBasket(basketId, basket);
        Guard.Against.EmptyBasketOnCheckout(basket.Items);

        var catalogItemsSpecification = new CatalogItemsSpecification(basket.Items.Select(item => item.CatalogItemId).ToArray());
        var catalogItems = await _itemRepository.ListAsync(catalogItemsSpecification);

        var items = basket.Items.Select(basketItem =>
        {
            var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);
            var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, _uriComposer.ComposePicUri(catalogItem.PictureUri));
            var orderItem = new OrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
            return orderItem;
        }).ToList();

        var order = new Order(basket.BuyerId, shippingAddress, items);
        await _orderRepository.AddAsync(order);

        var message = new ServiceBusMessage(JsonSerializer.Serialize(order));
        _logger.LogInformation("start sending to messagebus");
        await _sender.SendMessageAsync(message);
        _logger.LogInformation("sent to messagebus");


        _logger.LogInformation("start sending http request");
        var content = ToJson(order);
        string uri = "OrderItems";
        var result = await _httpClient.PostAsync($"{_funcUrl}{uri}", content);
        if (!result.IsSuccessStatusCode)
        {
            _logger.LogInformation("sending http request end");
        }

    }

    private ServiceBusSender buildSender()
    {
        string ServiceBusConnectionString = _configuration["ServiceBusConnectionString"];
        string QueueName = _configuration["itemsmessages"];
        var client = new ServiceBusClient(ServiceBusConnectionString);
        ServiceBusSender sender = client.CreateSender(QueueName);
        return sender;
    }
    private StringContent ToJson(object obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }
}
