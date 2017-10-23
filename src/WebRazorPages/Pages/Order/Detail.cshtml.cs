using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using ApplicationCore.Interfaces;
using System.Linq;

namespace Microsoft.eShopWeb.RazorPages.Pages.Order
{
    public class DetailModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public DetailModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderViewModel OrderDetails { get; set; } = new OrderViewModel();


        public async Task OnGet(int orderId)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(orderId);
            OrderDetails = new OrderViewModel()
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    Discount = 0,
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Status = "Pending",
                Total = order.Total()
            };
        }
    }
}
