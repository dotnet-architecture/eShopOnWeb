using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.eShopWeb;
using System.Threading.Tasks;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using System.Net;
using Microsoft.eShopWeb.PublicApi.OrderDetailEndpoints;

namespace PublicApiIntegrationTests.OrderItemEndepoints;

[TestClass]
public class GetOrderDetailByOrderIdEndpointTest
{
    [TestMethod]
    public async Task ReturnsOrderDetail()
    {
        var response = await ProgramTest.NewClient.GetAsync("api/OrderItem/1");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GetOrderDetailByOrderIdResponse>();

        Assert.IsNotNull(model);
        Assert.IsTrue(model.OrderItems.Count > 0);
        bool foundOrderItem = false;
        foreach (var orderItem in model.OrderItems)
        {
            if (orderItem.Units == 1 && orderItem.UnitPrice == 1)
            {
                foundOrderItem = true;
                break;
            }
        }
        Assert.IsTrue(foundOrderItem, $"OrderItem with Id {1} and OrderId {1} not found.");
    }
}
