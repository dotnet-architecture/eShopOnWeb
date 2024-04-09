using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using Microsoft.eShopWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using BlazorShared.Enums;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace PublicApiIntegrationTests.OrderEndPoints;

[TestClass]
public class UpdateOrderEndpointTest
{
    [TestMethod]
    public async Task UpdatesOrderGivenValidModel()
    {
        var request = new UpdateOrderRequest
        {
            Id = 1,
            OrderStatus = OrderStatus.Pending
        };
        
        var response = await ProgramTest.NewClient.PutAsJsonAsync("api/order-update", request);
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<UpdateOrderResponse>();

        Assert.AreEqual(1, model!.Order.Id);
        Assert.AreEqual(OrderStatus.Pending, model.Order.OrderStatus);
    }

    [TestMethod]
    public async Task ReturnsNotUpdatedGivenInvalidModels()
    {
        var response = await ProgramTest.NewClient.GetAsync("api/order-update");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
