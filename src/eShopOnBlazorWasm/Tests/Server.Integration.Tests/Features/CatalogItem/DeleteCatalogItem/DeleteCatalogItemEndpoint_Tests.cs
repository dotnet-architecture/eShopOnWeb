namespace DeleteCatalogItemEndpoint
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
    private readonly DeleteCatalogItemRequest DeleteCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      DeleteCatalogItemRequest = new DeleteCatalogItemRequest { CatalogItemId = 5 };
    }

    public async Task DeleteCatalogItemResponse()
    {
      DeleteCatalogItemResponse DeleteCatalogItemResponse =
        await GetJsonAsync<DeleteCatalogItemResponse>(DeleteCatalogItemRequest.RouteFactory);

      ValidateDeleteCatalogItemResponse(DeleteCatalogItemResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      DeleteCatalogItemRequest.CatalogItemId = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(DeleteCatalogItemRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(DeleteCatalogItemRequest.CatalogItemId));
    }

    private void ValidateDeleteCatalogItemResponse(DeleteCatalogItemResponse aDeleteCatalogItemResponse)
    {
      aDeleteCatalogItemResponse.RequestId.Should().Be(DeleteCatalogItemRequest.RequestId);
      // check Other properties here
    }
  }
}