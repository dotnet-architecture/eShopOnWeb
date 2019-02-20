using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    public class ApiCatalogControllerList : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public ApiCatalogControllerList(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsFirst10CatalogItems()
        {
            var response = await Client.GetAsync("/api/catalog/list");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.Equal(10, model.CatalogItems.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var response = await Client.GetAsync("/api/catalog/list?page=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.Equal(2, model.CatalogItems.Count());
        }
    }
}
