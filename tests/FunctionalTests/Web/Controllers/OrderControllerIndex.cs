using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class OrderIndexOnGet : IClassFixture<WebTestFixture>
    {
        public OrderIndexOnGet(WebTestFixture factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsRedirectGivenAnonymousUser()
        {
            var response = await Client.GetAsync("/order/my-orders");
            var redirectLocation = response.Headers.Location.OriginalString;

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Account/Login", redirectLocation);
        }
    }
}
