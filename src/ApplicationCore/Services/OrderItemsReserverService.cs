using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderItemsReserverService : IOrderItemsReserverService
    {
        private readonly HttpClient _httpClient;
        public OrderItemsReserverService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PlaceOrderAsync(Dictionary<string, int> items)
        {            
            var data = items.Keys.Select(key => new { Id = key, Quantity = items[key]});
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/OrderItemsReserver", httpContent);
            return;
        }
    }
}
