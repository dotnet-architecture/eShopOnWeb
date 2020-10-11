using Microsoft.eShopWeb.FunctionalTests.PublicApi;
using Microsoft.eShopWeb.FunctionalTests.Web.Api;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class DeleteEndpoint : IClassFixture<ApiTestFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public DeleteEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsSuccessGivenValidIdAndAdminUserToken()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.DeleteAsync("api/catalog-items/12");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<DeleteCatalogItemResponse>();

            Assert.Equal("Deleted", model.Status);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidIdAndAdminUserToken()
        {
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.DeleteAsync("api/catalog-items/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
