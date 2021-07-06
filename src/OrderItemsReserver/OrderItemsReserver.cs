using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using OrderItemsReserver.Helpers;

namespace OrderItemsReserver
{
    public static class OrderItemsReserver
    {
        [FunctionName("OrderItemsReserver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<OrderItem> data = JsonConvert.DeserializeObject<List<OrderItem>>(requestBody);

            string cloudStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process);
            string containerName = Environment.GetEnvironmentVariable("ContainerName", EnvironmentVariableTarget.Process);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cloudStorage);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{DateTime.UtcNow.ToUnixTimestamp()}.json");
            await blockBlob.UploadTextAsync(JsonConvert.SerializeObject(data));
            
            return new OkObjectResult("Success");
        }
    }
}
