using Microsoft.eShopWeb.FunctionalTests.PublicApi;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class GetByIdEndpoint : IClassFixture<ApiTestFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public GetByIdEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsItemGivenValidId()
        {
            var response = await Client.GetAsync("api/catalog-items/5");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<GetByIdCatalogItemResponse>();

            Assert.Equal(5, model.CatalogItem.Id);
            Assert.Equal("Roslyn Red Sheet", model.CatalogItem.Name);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var response = await Client.GetAsync("api/catalog-items/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
