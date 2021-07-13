using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DeliveryOrderProcessor;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace Functions
{
    public static class DeliveryOrderProcessor
    {
        [FunctionName("DeliveryOrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Order data = JsonConvert.DeserializeObject<Order>(requestBody);

            string accountEndpoint = Environment.GetEnvironmentVariable("AccountEndpoint", EnvironmentVariableTarget.Process);
            string accountKey = Environment.GetEnvironmentVariable("AccountKey", EnvironmentVariableTarget.Process);

            string dbName = "eShop";
            string collectionName = "Orders";
            DocumentClient client = new DocumentClient(new Uri(accountEndpoint), accountKey);
            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName });
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = collectionName });
            await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, collectionName), data);

            return new OkObjectResult("Success");
        }
    }
}
