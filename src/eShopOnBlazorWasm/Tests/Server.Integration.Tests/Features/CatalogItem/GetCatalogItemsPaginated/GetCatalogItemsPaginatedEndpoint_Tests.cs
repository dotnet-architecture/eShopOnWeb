namespace GetCatalogItemsPaginatedEndpoint
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

    private void ValidateGetCatalogItemsPaginatedResponse(GetCatalogItemsPaginatedResponse aGetCatalogItemsPaginatedResponse)
    {
      aGetCatalogItemsPaginatedResponse.RequestId.ShouldBe(GetCatalogItemsPaginatedRequest.Id);
      aGetCatalogItemsPaginatedResponse.RequestId.Should().Be(GetCatalogItemsPaginatedRequest.Id);
      //aGetCatalogItemsPaginatedResponse.CatalogItems.Count.ShouldBe(GetCatalogItemsPaginatedRequest.PageSize);
      //aGetCatalogItemsPaginatedResponse.CatalogItems.Count.Should().Be(GetCatalogItemsPaginatedRequest.PageSize);

    }

  }
}