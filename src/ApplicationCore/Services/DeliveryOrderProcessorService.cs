using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class DeliveryOrderProcessorService : IDeliveryOrderProcessorService
    {
        private readonly HttpClient _httpClient;

        public DeliveryOrderProcessorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            var data = new
            {
                OrderId = order.Id,
                ShippingAddress = $"{order.ShipToAddress.Country}, {order.ShipToAddress.City}, {order.ShipToAddress.Street}, {order.ShipToAddress.ZipCode}",
                Items = order.OrderItems.Select(oi => new { Id = oi.Id, Units = oi.Units }),
                Total = order.Total()
            };
            Console.WriteLine("Order: ", JsonConvert.SerializeObject(data));
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/DeliveryOrderProcessor", httpContent);
            return;
        }
    }
}
