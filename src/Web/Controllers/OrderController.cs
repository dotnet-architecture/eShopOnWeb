using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.ViewModels;
using System.Collections.Generic;
using System;
using ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {

        public OrderController() { }
        
        public async Task<IActionResult> Index()
        {
            var orders = new List<OrderViewModel>();
            orders.Add(GetOrder());
            return View(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Detail(string orderId)
        {
            var order = GetOrder();
            return View(order);
        }

        private OrderViewModel GetOrder()
        {
            var order = new OrderViewModel()
            {
                OrderDate = DateTimeOffset.Now.AddDays(-1),
                OrderNumber = "12354",
                Status = "Submitted",
                Total = 123.45m,
                ShippingAddress = new Address("123 Main St.", "Kent", "OH", "United States", "44240")
            };

            order.OrderItems.Add(new OrderItemViewModel()
            {
                ProductId = 1,
                PictureUrl = "",
                ProductName = "Something",
                UnitPrice = 5.05m,
                Units = 2
            });

            return order;
        }
    }
}
