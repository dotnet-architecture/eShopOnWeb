using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly ILogger<OrderViewModelService> _logger;
    private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;


    public OrderViewModelService(IUriComposer uriComposer, IRepository<Order> orderRepository, ILogger<OrderViewModelService> logger)
    {
        _uriComposer = uriComposer;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<List<OrderViewModel>> GetOrderList()
    {
        var orders = await _orderRepository.ListAsync();
        var model = new List<OrderViewModel>();
        foreach (var order in orders)
        {
            model.Add(new OrderViewModel
            {
                OrderNumber = order.Id,
                OrderDate = order.OrderDate,
                BuyerId = order.BuyerId,
                Total = order.Total(),
                OrderStatus = order.OrderStatus
            });
        }
        return model;
    }

    public async Task<OrderViewModel> GetOrderById(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        var model = new OrderViewModel
        {
            OrderNumber = order.Id,
            OrderDate = order.OrderDate,
            BuyerId = order.BuyerId,
            Total = order.Total(),
            OrderStatus = order.OrderStatus
        };
        return model;
    }

    public async Task<List<OrderItemViewModel>> GetOrderItems(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        var model = new List<OrderItemViewModel>();
        foreach (var item in order.OrderItems)
        {
            model.Add(new OrderItemViewModel
            {
                ProductId = item.ItemOrdered.CatalogItemId,
                ProductName = item.ItemOrdered.ProductName,
                UnitPrice = item.UnitPrice,
                Units = item.Units,
                PictureUrl = _uriComposer.ComposePicUri(item.ItemOrdered.PictureUri)
            });
        }
        return model;
    }
}
