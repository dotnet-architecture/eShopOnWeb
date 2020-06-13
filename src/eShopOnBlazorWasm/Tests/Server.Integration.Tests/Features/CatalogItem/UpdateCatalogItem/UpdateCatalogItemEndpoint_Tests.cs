namespace UpdateCatalogItemEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using System.Net.Http;
  using System.Net;

  public class Returns : BaseTest
  {
    private readonly UpdateCatalogItemRequest UpdateCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      UpdateCatalogItemRequest = new UpdateCatalogItemRequest { CatalogItemId = 5 };
    }

    public async Task UpdatedCatalogItem()
    {
      UpdateCatalogItemResponse updateCatalogItemResponse =
        await GetJsonAsync<UpdateCatalogItemResponse>(UpdateCatalogItemRequest.RouteFactory);

      ValidateUpdateCatalogItemResponse(updateCatalogItemResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      UpdateCatalogItemRequest.CatalogItemId = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(UpdateCatalogItemRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(UpdateCatalogItemRequest.CatalogItemId));
    }

    private void ValidateUpdateCatalogItemResponse(UpdateCatalogItemResponse aUpdateCatalogItemResponse)
    {
      aUpdateCatalogItemResponse.RequestId.Should().Be(UpdateCatalogItemRequest.RequestId);
    }

  }
}