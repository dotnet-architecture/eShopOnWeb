namespace FindCatalogItemEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Shouldly;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using eShopOnBlazorWasm.Features.Catalog;

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

    private void ValidateFindCatalogItemResponse(FindCatalogItemResponse aFindCatalogItemResponse)
    {
      aFindCatalogItemResponse.RequestId.ShouldBe(FindCatalogItemRequest.Id);
      aFindCatalogItemResponse.RequestId.Should().Be(FindCatalogItemRequest.Id);
    }

  }
}