using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    public class AccountControllerSignIn : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public AccountControllerSignIn(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsSignInScreenOnGet()
        {
            var response = await Client.GetAsync("/account/sign-in");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("demouser@microsoft.com", stringResponse);
        }

        // TODO: Finish this test.
        //[Fact]
        public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
        {
            var response = await Client.GetAsync("/account/sign-in");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            // TODO: Get the token from a Get call
            // Ref: https://buildmeasurelearn.wordpress.com/2016/11/23/handling-asp-net-mvcs-anti-forgery-tokens-when-load-testing-with-jmeter/


            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Email", "demouser%40microsoft.com"));
            keyValues.Add(new KeyValuePair<string, string>("Password", "Pass%40word1"));

            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", "CfDJ8Obhlq65OzlDkoBvsSX0tgwbZmZYzUj6hduxJPKiEytYZ4gH8MBOlTGA3ezMmVBZxXYf0mzzV4PuoHXgtlbQWvBKkTzIGofnLHFTOV_Vj59NWJYAQ9L7bp38xDC_axw-d94EKgJYt2xCb8E9DDSaPbE"));
            var formContent = new FormUrlEncodedContent(keyValues);

            var response = await Client.PostAsync("/account/sign-in", formContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            //Assert.Contains("demouser@microsoft.com", stringResponse);
        }
    }
}
