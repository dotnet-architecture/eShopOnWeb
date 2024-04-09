using System.Collections.Generic;
using Microsoft.eShopWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using System.Net;

namespace PublicApiIntegrationTests.OrderEndPoints;

[TestClass]
public class GetOrderListEndpointTest
{
    [TestMethod]
    public async Task ReturnsOrderList()
    {
        var response = await ProgramTest.NewClient.GetAsync("api/orders");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GetOrderListResponse>();


    }

    [TestMethod]
    public async Task ReturnsNotFoundGivenInvalidId()
    {
        var response = await ProgramTest.NewClient.GetAsync("api/orders/0");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
