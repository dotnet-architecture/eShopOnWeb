using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Azure.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IRepository<Basket> _basketRepository;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly HttpClient _httpClient;
    private readonly ITopicClient _topicClient;



    public OrderService(IRepository<Basket> basketRepository,
        IRepository<CatalogItem> itemRepository,
        IRepository<Order> orderRepository,
        IUriComposer uriComposer,
        HttpClient httpClient,
        ITopicClient topicClient)
    {
        _orderRepository = orderRepository;
        _uriComposer = uriComposer;
        _basketRepository = basketRepository;
        _itemRepository = itemRepository;
        _httpClient = httpClient;
        _topicClient = topicClient;

    }

    public async Task CreateOrderAsync(int basketId, Address shippingAddress)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

        Guard.Against.Null(basket, nameof(basket));
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

        await SendOrderToReservation(order);
        //await SendOrderToDelivery(order);
    }
    private async Task SendOrderToDelivery(Order order)
    {
        try
        {
            string functionUrl = "https://createdeliveryitemfunc.azurewebsites.net/api/CreateDeliveryItem";

            var itemToDelivery = new
            {
                OrderId = order.Id,
                Items = order.OrderItems.Select(orderItem => orderItem.ItemOrdered.ProductName).ToList(),
                ShippingAddress = order.ShipToAddress.GetFullAddress(),
                FinalPrice = order.Total()
            };
        string jsonData = JsonConvert.SerializeObject(itemToDelivery);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(functionUrl, content);
        }
        catch
        {
            throw;
        }
    }
    //private async Task SendOrderToReservation(Order order)
    //{
    //    string functionUrl = " http://localhost:7271/api/OrderItemsReserver";

    //    var itemReservation = new
    //    {
    //        orderId = order.Id,
    //        orderItems = order.OrderItems.Select(orderItem => new
    //        {
    //            itemId = orderItem.ItemOrdered.CatalogItemId,
    //            quantity = orderItem.Units
    //        }).ToList()
    //    };
    //    string jsonData = JsonConvert.SerializeObject(itemReservation);
    //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
    //    await _httpClient.PostAsync(functionUrl, content);
    //}

    private async Task SendOrderToReservation(Order order)
    {
        try
        {
            var itemReservation = new
            {
                orderId = order.Id,
                orderItems = order.OrderItems.Select(orderItem => new
                {
                    itemId = orderItem.ItemOrdered.CatalogItemId,
                    quantity = orderItem.Units
                }).ToList()
            };

            string messageBody = JsonConvert.SerializeObject(itemReservation);
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageBody);
            Message message = new Message(messageBytes);

            await _topicClient.SendAsync(message);
        }
        catch
        {
            throw;
        }

    }
}
