namespace FindCatalogItemEndpoint
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
    private readonly FindCatalogItemRequest FindCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      FindCatalogItemRequest = new FindCatalogItemRequest { };
    }

    public async Task CatalogItem_Given_ItemExists()
    {
      FindCatalogItemResponse findCatalogItemResponse =
        await GetJsonAsync<FindCatalogItemResponse>(FindCatalogItemRequest.RouteFactory);

      ValidateFindCatalogItemResponse(findCatalogItemResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      FindCatalogItemRequest.CatalogBrandId = -1;
      FindCatalogItemRequest.CatalogTypeId = -10;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(FindCatalogItemRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(FindCatalogItemRequest.CatalogBrandId));
      json.Should().Contain(nameof(FindCatalogItemRequest.CatalogTypeId));
    }

    private void ValidateFindCatalogItemResponse(FindCatalogItemResponse aFindCatalogItemResponse)
    {
      aFindCatalogItemResponse.CorrelationId.Should().Be(FindCatalogItemRequest.CorrelationId);
    }

  }
}