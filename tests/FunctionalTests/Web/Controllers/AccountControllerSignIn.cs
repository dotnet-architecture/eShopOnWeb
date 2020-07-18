using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class AccountControllerSignIn : IClassFixture<WebTestFixture>
    {
        public AccountControllerSignIn(WebTestFixture factory)
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
            var response = await Client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("demouser@microsoft.com", stringResponse);
        }

        [Fact]
        public void RegexMatchesValidRequestVerificationToken()
        {
            // TODO: Move to a unit test
            // TODO: Move regex to a constant in test project
            var input = @"<input name=""__RequestVerificationToken"" type=""hidden"" value=""CfDJ8Obhlq65OzlDkoBvsSX0tgxFUkIZ_qDDSt49D_StnYwphIyXO4zxfjopCWsygfOkngsL6P0tPmS2HTB1oYW-p_JzE0_MCFb7tF9Ol_qoOg_IC_yTjBNChF0qRgoZPmKYOIJigg7e2rsBsmMZDTdbnGo"" /><input name=""RememberMe"" type=""hidden"" value=""false"" /></form>";
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            var group = match.Groups.Values.LastOrDefault();
            Assert.NotNull(group);
            Assert.True(group.Value.Length > 50);
        }

        [Fact]
        public async Task ReturnsFormWithRequestVerificationToken()
        {
            var response = await Client.GetAsync("/identity/account/login");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            string token = GetRequestVerificationToken(stringResponse);
            Assert.True(token.Length > 50);
        }

        private string GetRequestVerificationToken(string input)
        {
            string regexpression = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
            var regex = new Regex(regexpression);
            var match = regex.Match(input);
            return match.Groups.Values.LastOrDefault().Value;
        }

        [Fact]
        public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
        {
            var getResponse = await Client.GetAsync("/identity/account/login");
            getResponse.EnsureSuccessStatusCode();
            var stringResponse1 = await getResponse.Content.ReadAsStringAsync();
            string token = GetRequestVerificationToken(stringResponse1);

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Email", "demouser@microsoft.com"));
            keyValues.Add(new KeyValuePair<string, string>("Password", "Pass@word1"));

            keyValues.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));
            var formContent = new FormUrlEncodedContent(keyValues);

            var postResponse = await Client.PostAsync("/identity/account/login", formContent);
            Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
            Assert.Equal(new System.Uri("/", UriKind.Relative), postResponse.Headers.Location);
        }
    }
}
