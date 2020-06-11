namespace GetCatalogItemsPaginatedHandler
{
  using Shouldly;
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;
  using eShopOnBlazorWasm.Features.Catalog;

  public class Handle_Returns : BaseTest
  {
    private readonly GetCatalogItemsPaginatedRequest GetCatalogItemsPaginatedRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogItemsPaginatedRequest = new GetCatalogItemsPaginatedRequest { PageIndex = 2, PageSize = 10 };
    }

    public async Task _10CatalogItems_Given_PageSize_10_Requested()
    {
      GetCatalogItemsPaginatedResponse getCatalogItemsPaginatedResponse = await Send(GetCatalogItemsPaginatedRequest);

      ValidateGetCatalogItemsPaginatedResponse(getCatalogItemsPaginatedResponse);
    }

    private void ValidateGetCatalogItemsPaginatedResponse(GetCatalogItemsPaginatedResponse aGetCatalogItemsPaginatedResponse)
    {
      aGetCatalogItemsPaginatedResponse.RequestId.ShouldBe(GetCatalogItemsPaginatedRequest.Id);
      aGetCatalogItemsPaginatedResponse.RequestId.Should().Be(GetCatalogItemsPaginatedRequest.Id);
    }

  }
}