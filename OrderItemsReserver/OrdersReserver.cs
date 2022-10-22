using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace OrderItemsReserver
{
    public class OrdersReserver
    {
        [FunctionName("OrderItemsDeliveryServiceRun")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var content = await req.Content.ReadAsStringAsync();
            try
            {
                await SaveToCosmosDb(content);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }

            return new OkObjectResult("This HTTP triggered function executed successfully.");
        }

        private static async Task SaveToCosmosDb(string contentToSave)
        {
            var _endpointUri = Environment.GetEnvironmentVariable("CosmosDbUri");
            var _primaryKey = Environment.GetEnvironmentVariable("CosmosDbPrimaryKey");

            using (var cosmosDbClient = new CosmosClient(_endpointUri, _primaryKey))
            {
                var dbResponse = await cosmosDbClient.CreateDatabaseIfNotExistsAsync("DeliveryService");
                var targetDb = dbResponse.Database;
                var indexingPolicy = new IndexingPolicy
                {
                    IndexingMode = IndexingMode.Consistent,
                    Automatic = true,
                    IncludedPaths = { new IncludedPath { Path = "/*" } }
                };
                var containerProperties = new ContainerProperties("Orders", "/ShippingAddress")
                {
                    IndexingPolicy = indexingPolicy,
                };
                var containerResponse = await targetDb.CreateContainerIfNotExistsAsync(containerProperties);
                var container = containerResponse.Container;

                var item = JsonConvert.DeserializeObject<OrderDeliveryModel>(contentToSave);
                await container.CreateItemAsync(item);
            }
        }
    }
}
