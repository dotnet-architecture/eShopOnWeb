using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace PublicApiIntegrationTests.CatalogItemEndpoints;

[TestClass]
public class CatalogItemListPagedEndpoint
{
    [TestMethod]
    public async Task ReturnsFirst10CatalogItems()
    {
        var client = ProgramTest.NewClient;
        var response = await client.GetAsync("/api/catalog-items?pageSize=10");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<CatalogIndexViewModel>();

        Assert.AreEqual(10, model!.CatalogItems.Count());
    }

    [TestMethod]
    public async Task ReturnsCorrectCatalogItemsGivenPageIndex1()
    {

        var pageSize = 10;
        var pageIndex = 1;

        var client = ProgramTest.NewClient;
        var response = await client.GetAsync($"/api/catalog-items");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<ListPagedCatalogItemResponse>();
        var totalItem = model!.CatalogItems.Count();

        var response2 = await client.GetAsync($"/api/catalog-items?pageSize={pageSize}&pageIndex={pageIndex}");
        response.EnsureSuccessStatusCode();
        var stringResponse2 = await response2.Content.ReadAsStringAsync();
        var model2 = stringResponse2.FromJson<ListPagedCatalogItemResponse>();

        var totalExpected = totalItem - (pageSize * pageIndex);

        Assert.AreEqual(totalExpected, model2!.CatalogItems.Count());
    }

    [DataTestMethod]
    [DataRow("catalog-items")]
    [DataRow("catalog-brands")]
    [DataRow("catalog-types")]
    [DataRow("catalog-items/1")]
    public async Task SuccessFullMutipleParallelCall(string endpointName)
    {
        var client = ProgramTest.NewClient;
        var tasks = new List<Task<HttpResponseMessage>>();

        for (int i = 0; i < 100; i++)
        {
            var task = client.GetAsync($"/api/{endpointName}");
            tasks.Add(task);
        }
        await Task.WhenAll(tasks.ToList());
        var totalKO = tasks.Count(t => t.Result.StatusCode != HttpStatusCode.OK);

        Assert.AreEqual(0, totalKO);
    }
}
