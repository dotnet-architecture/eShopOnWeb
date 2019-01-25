using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.Web;
using System;
using System.Collections.Generic;
using System.Net;
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
        [Fact]
        public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
        {
            //var response = await Client.GetAsync("/account/sign-in");
            //response.EnsureSuccessStatusCode();
            //var stringResponse = await response.Content.ReadAsStringAsync();
            // TODO: Get the token from a Get call
            // Ref: https://buildmeasurelearn.wordpress.com/2016/11/23/handling-asp-net-mvcs-anti-forgery-tokens-when-load-testing-with-jmeter/


            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Email", "demouser@microsoft.com"));
            keyValues.Add(new KeyValuePair<string, string>("Password", "Pass@word1"));

            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", "CfDJ8Obhlq65OzlDkoBvsSX0tgyXhgITd4pD1OocDNYfbIeOkBMVLl3SmcZjyHLFqAlfvNOcWnV73G520010NOL1VaHRODGXZxTNjkIOjOi36YW3Fs5Bb9K9baf0hLFrmFI4P1w-64FURukDzaWRGl0Tzw0"));
            var formContent = new FormUrlEncodedContent(keyValues);

            var response = await Client.PostAsync("/account/sign-in", formContent);
            //response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal(new System.Uri("/", UriKind.Relative), response.Headers.Location);
        }
    }
}
