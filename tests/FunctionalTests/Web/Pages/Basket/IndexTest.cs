using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Pages.Basket;

[Collection("Sequential")]
public class IndexTest : IClassFixture<TestApplication>
{
    public IndexTest(TestApplication factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    public HttpClient Client { get; }


    [Fact]
    public async Task OnPostUpdateTo50Successfully()
    {
        // Load Home Page
        var response = await Client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var stringResponse1 = await response.Content.ReadAsStringAsync();

        string token = WebPageHelpers.GetRequestVerificationToken(stringResponse1);

        // Add Item to Cart
        var keyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>("__RequestVerificationToken", token)
        };
        var formContent = new FormUrlEncodedContent(keyValues);
        var postResponse = await Client.PostAsync("/basket/index", formContent);
        postResponse.EnsureSuccessStatusCode();
        var stringResponse = await postResponse.Content.ReadAsStringAsync();
        Assert.Contains(".NET Black &amp; White Mug", stringResponse);

        //Update
        var updateKeyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Items[0].Id", WebPageHelpers.GetId(stringResponse)),
            new KeyValuePair<string, string>("Items[0].Quantity", "49"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(stringResponse))
        };
        var updateContent = new FormUrlEncodedContent(updateKeyValues);
        var updateResponse = await Client.PostAsync("/basket/update", updateContent);

        var stringUpdateResponse = await updateResponse.Content.ReadAsStringAsync();

        Assert.Contains("/basket/update", updateResponse!.RequestMessage!.RequestUri!.ToString()!);
        decimal expectedTotalAmount = 416.50M;
        Assert.Contains(expectedTotalAmount.ToString("N2"), stringUpdateResponse);
    }

    [Fact]
    public async Task OnPostUpdateTo0EmptyBasket()
    {
        // Load Home Page
        var response = await Client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var stringResponse1 = await response.Content.ReadAsStringAsync();

        string token = WebPageHelpers.GetRequestVerificationToken(stringResponse1);

        // Add Item to Cart
        var keyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>("__RequestVerificationToken", token)
        };
        var formContent = new FormUrlEncodedContent(keyValues);
        var postResponse = await Client.PostAsync("/basket/index", formContent);
        postResponse.EnsureSuccessStatusCode();
        var stringResponse = await postResponse.Content.ReadAsStringAsync();
        Assert.Contains(".NET Black &amp; White Mug", stringResponse);

        //Update
        var updateKeyValues = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Items[0].Id", WebPageHelpers.GetId(stringResponse)),
            new KeyValuePair<string, string>("Items[0].Quantity", "0"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(stringResponse))
        };
        var updateContent = new FormUrlEncodedContent(updateKeyValues);
        var updateResponse = await Client.PostAsync("/basket/update", updateContent);

        var stringUpdateResponse = await updateResponse.Content.ReadAsStringAsync();

        Assert.Contains("/basket/update", updateResponse!.RequestMessage!.RequestUri!.ToString()!);
        Assert.Contains("Basket is empty", stringUpdateResponse);
    }
}
