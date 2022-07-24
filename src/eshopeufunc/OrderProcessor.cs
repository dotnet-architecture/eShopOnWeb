using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace eshopeufunc
{
    public static class OrderProcessor
    {
        [FunctionName("OrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order-details")] HttpRequest req,
            [CosmosDB(
                databaseName: "OrdersProcessing",
                collectionName: "OrdersProcessing",
                ConnectionStringSetting = "CosmosDBConnection")]
                IAsyncCollector<OrderDetailsProcessingDto> orderDetails,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var data = await JsonSerializer.DeserializeAsync<OrderDetailsProcessingDto>(
                req.Body,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            await orderDetails.AddAsync(data);

            return new OkResult();
        }
    }

    public record OrderDetailsProcessingDto(
    decimal FinalPrice,
    ShippingAddressProcessingDto ShippingAddress,
    IEnumerable<OrderItemProcessingDto> OrderItems);

    public record ShippingAddressProcessingDto(
        string Street,
        string City,
        string State,
        string Country,
        string ZipCode);

    public record OrderItemProcessingDto(
        string ProductName,
        decimal UnitPrice);
}
