using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApiIntegrationTests.CatalogItemEndpoints
{
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

            Assert.AreEqual(10, model.CatalogItems.Count());
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
            var totalItem = model.CatalogItems.Count();

            var response2 = await client.GetAsync($"/api/catalog-items?pageSize={pageSize}&pageIndex={pageIndex}");
            response.EnsureSuccessStatusCode();
            var stringResponse2 = await response2.Content.ReadAsStringAsync();
            var model2 = stringResponse2.FromJson<ListPagedCatalogItemResponse>();

            var totalExpected = totalItem - (pageSize * pageIndex);

            Assert.AreEqual(totalExpected, model2.CatalogItems.Count());
        }
    }
}
