namespace GetCatalogItemEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Net;
  using System.Net.Http;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;

  public class Returns : BaseTest
  {
    private readonly GetCatalogItemRequest GetCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogItemRequest = new GetCatalogItemRequest { CatalogItemId = 10 };
    }

    public async Task GetCatalogItemResponse()
    {
      GetCatalogItemResponse getCatalogItemResponse =
        await GetJsonAsync<GetCatalogItemResponse>(GetCatalogItemRequest.RouteFactory);

      ValidateGetCatalogItemResponse(getCatalogItemResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      GetCatalogItemRequest.CatalogItemId = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(GetCatalogItemRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(GetCatalogItemRequest.CatalogItemId));
    }

    private void ValidateGetCatalogItemResponse(GetCatalogItemResponse aGetCatalogItemResponse)
    {
      aGetCatalogItemResponse.CorrelationId.Should().Be(GetCatalogItemRequest.CorrelationId);
      // check Other properties here
    }
  }
}