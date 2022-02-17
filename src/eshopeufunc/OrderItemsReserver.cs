using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace eshopeufunc;

public static class OrderItemsReserver
{
    [FunctionName("OrderItemsReserver")]
    public static async Task Run(
        [ServiceBusTrigger("ordersqueue", Connection = "ServiceBusConnection")] string myQueueItem,
        [Blob("order-requests/{sys.randguid}", FileAccess.Write), StorageAccount("AzureWebJobsStorage")] Stream blobStream,
        ILogger log)
    {
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

        await blobStream.WriteAsync(Encoding.UTF8.GetBytes(myQueueItem));
    }
}
