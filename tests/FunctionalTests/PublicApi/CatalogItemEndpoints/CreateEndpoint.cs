using Microsoft.eShopWeb.FunctionalTests.PublicApi;
using Microsoft.eShopWeb.FunctionalTests.Web.Api;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class CreateEndpoint : IClassFixture<ApiTestFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private int _testBrandId = 1;
        private int _testTypeId = 2;
        private string _testDescription = "test description";
        private string _testName = "test name";        
        private decimal _testPrice = 1.23m;

        public CreateEndpoint(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsNotAuthorizedGivenNormalUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var token = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("api/catalog-items", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsSuccessGivenValidNewItemAndAdminUserToken()
        {
            var jsonContent = GetValidNewItemJson();
            var adminToken = ApiTokenHelper.GetAdminUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await Client.PostAsync("api/catalog-items", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateCatalogItemResponse>();

            Assert.Equal(_testBrandId, model.CatalogItem.CatalogBrandId);
            Assert.Equal(_testTypeId, model.CatalogItem.CatalogTypeId);
            Assert.Equal(_testDescription, model.CatalogItem.Description);
            Assert.Equal(_testName, model.CatalogItem.Name);            
            Assert.Equal(_testPrice, model.CatalogItem.Price);
        }

        private StringContent GetValidNewItemJson()
        {
            var request = new CreateCatalogItemRequest()
            {
                CatalogBrandId = _testBrandId,
                CatalogTypeId = _testTypeId,
                Description = _testDescription,
                Name = _testName,                
                Price = _testPrice
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}
