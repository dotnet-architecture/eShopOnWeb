using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Microsoft.eShopWeb.RazorPages.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IndexModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OrderSummary> Orders { get; set; } = new List<OrderSummary>();

        public class OrderSummary
        {
            public int OrderNumber { get; set; }
            public DateTimeOffset OrderDate { get; set; }
            public decimal Total { get; set; }
            public string Status { get; set; }
        }

        public async Task OnGet()
        {
            var orders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));

            Orders = orders
                .Select(o => new OrderSummary()
                {
                    OrderDate = o.OrderDate,
                    OrderNumber = o.Id,
                    Status = "Pending",
                    Total = o.Total()

                }).ToList();
        }
    }
}
