using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;


namespace Microsoft.eShopWeb.Web.Features.Orders;

public class GetOrdersListHandler : IRequestHandler<GetOrdersList, IEnumerable<OrderViewModel>>
{
    private readonly IReadRepository<Order> _orderRepository;

    public GetOrdersListHandler(IReadRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersList request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.ListAsync();
        return orders.Select(o => new OrderViewModel
        {
            OrderNumber = o.Id,
            OrderDate = o.OrderDate,
            ShippingAddress = o.ShipToAddress,
            OrderStatus = o.OrderStatus,
            Total = o.Total()
        });
    }
}
