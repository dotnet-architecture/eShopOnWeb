using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTestsRazorPages.WebRazorPages.Pages
{
    public class AuthorizationTest : BasePageTest
    {
        [Fact]
        public async Task RedirectToLoginPage()
        {
            var response = await _client.GetAsync("/Basket/Checkout");

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);

            Assert.Equal("/Account/Signin?ReturnUrl=%2FBasket%2FCheckout", response.Headers.Location.PathAndQuery);
        }
    }
}
