using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailsHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var customerOrders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(request.UserName));
            var order = customerOrders.FirstOrDefault(o => o.Id == request.OrderId);

            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Total = order.Total()
            };
        }
    }
}
