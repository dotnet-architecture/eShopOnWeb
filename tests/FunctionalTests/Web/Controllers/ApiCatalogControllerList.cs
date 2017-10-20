using System.IO;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.eShopWeb.ViewModels;
using System.Linq;

namespace FunctionalTests.Web.Controllers
{
    public class ApiCatalogControllerList : BaseWebTest
    {
        [Fact]
        public async Task ReturnsFirst10CatalogItems()
        {
            var response = await _client.GetAsync("/api/catalog/list");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.Equal(10, model.CatalogItems.Count());
        }

        [Fact]
        public async Task ReturnsLast2CatalogItemsGivenPageIndex1()
        {
            var response = await _client.GetAsync("/api/catalog/list?page=1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CatalogIndexViewModel>(stringResponse);

            Assert.Equal(2, model.CatalogItems.Count());
        }
    }
}
