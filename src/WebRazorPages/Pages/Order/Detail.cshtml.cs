using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Interfaces;
using System.Linq;
using System;
using ApplicationCore.Entities.OrderAggregate;
using System.Collections.Generic;

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

        public class OrderViewModel
        {
            public int OrderNumber { get; set; }
            public DateTimeOffset OrderDate { get; set; }
            public decimal Total { get; set; }
            public string Status { get; set; }

            public Address ShippingAddress { get; set; }

            public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
        }

        public class OrderItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
            public int Units { get; set; }
            public string PictureUrl { get; set; }
        }

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
