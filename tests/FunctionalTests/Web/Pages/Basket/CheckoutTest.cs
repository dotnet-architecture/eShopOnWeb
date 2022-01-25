using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Pages.Basket;

[Collection("Sequential")]
public class CheckoutTest : IClassFixture<TestApplication>
{
    public CheckoutTest(TestApplication factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    public HttpClient Client { get; }

    [Fact]
    public async Task SucessfullyPay()
    {

        // Load Home Page
        var response = await Client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        // Add Item to Cart
        var keyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>("price", "19.49"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(stringResponse))
        };
        var formContent = new FormUrlEncodedContent(keyValues);
        var postResponse = await Client.PostAsync("/basket/index", formContent);
        postResponse.EnsureSuccessStatusCode();
        var stringPostResponse = await postResponse.Content.ReadAsStringAsync();
        Assert.Contains(".NET Black &amp; White Mug", stringPostResponse);

        //Load login page
        var loginResponse = await Client.GetAsync("/Identity/Account/Login");
        var longinKeyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("password", "Pass@word1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(await loginResponse.Content.ReadAsStringAsync()))
        };
        var loginFormContent = new FormUrlEncodedContent(longinKeyValues);
        var loginPostResponse = await Client.PostAsync("/Identity/Account/Login?ReturnUrl=%2FBasket%2FCheckout", loginFormContent);
        var loginStringResponse = await loginPostResponse.Content.ReadAsStringAsync();

        //Basket checkout (Pay now)
        var checkOutKeyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Items[0].Id", "2"),
            new KeyValuePair<string, string>("Items[0].Quantity", "1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(loginStringResponse))
        };
        var checkOutContent = new FormUrlEncodedContent(checkOutKeyValues);     
        var checkOutResponse = await Client.PostAsync("/basket/checkout", checkOutContent);
        var stringCheckOutResponse = await checkOutResponse.Content.ReadAsStringAsync();

        Assert.Contains("/Basket/Success", checkOutResponse.RequestMessage.RequestUri.ToString());
        Assert.Contains("Thanks for your Order!", stringCheckOutResponse);
    }
}
