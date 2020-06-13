namespace GetCatalogItemsPaginatedEndpoint
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
    private readonly GetCatalogItemsPaginatedRequest GetCatalogItemsPaginatedRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogItemsPaginatedRequest = new GetCatalogItemsPaginatedRequest { PageIndex = 2, PageSize = 10 };
    }

    public async Task _10CatalogItems_Given_PageSize_10_Requested()
    {
      GetCatalogItemsPaginatedResponse getCatalogItemsPaginatedResponse =
        await GetJsonAsync<GetCatalogItemsPaginatedResponse>(GetCatalogItemsPaginatedRequest.RouteFactory);

      ValidateGetCatalogItemsPaginatedResponse(getCatalogItemsPaginatedResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      GetCatalogItemsPaginatedRequest.PageIndex = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(GetCatalogItemsPaginatedRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(GetCatalogItemsPaginatedRequest.PageIndex));
    }

    private void ValidateGetCatalogItemsPaginatedResponse(GetCatalogItemsPaginatedResponse aGetCatalogItemsPaginatedResponse)
    {
      aGetCatalogItemsPaginatedResponse.RequestId.Should().Be(GetCatalogItemsPaginatedRequest.RequestId);
      aGetCatalogItemsPaginatedResponse.CatalogItems.Count.Should().Be(GetCatalogItemsPaginatedRequest.PageSize);
      // check Other properties here
    }
  }
}