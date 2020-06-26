using Microsoft.eShopWeb.Web.API.CatalogItemEndpoints;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<WebTestFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public CreateEndpoint(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsSuccessGivenValidId()
        {
            var testBrandId = 1;
            var testTypeId = 2;
            var testDescription = "test description";
            var testName = "test name";
            var testUri = "test uri";
            var testPrice = 1.23m;

            var request = new CreateCatalogItemRequest() 
            { 
                CatalogBrandId = testBrandId,
                CatalogTypeId = testTypeId,
                Description = testDescription,
                 Name = testName,
                 PictureUri = testUri,
                 Price = testPrice
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/catalog-items", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateCatalogItemResponse>();

            Assert.Equal(testBrandId, model.CatalogItem.CatalogBrandId);
            Assert.Equal(testTypeId, model.CatalogItem.CatalogTypeId);
            Assert.Equal(testDescription, model.CatalogItem.Description);
            Assert.Equal(testName, model.CatalogItem.Name);
            Assert.Equal(testUri, model.CatalogItem.PictureUri);
            Assert.Equal(testPrice, model.CatalogItem.Price);
        }
    }
}
