namespace FindCatalogItemHandler
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
    private readonly FindCatalogItemRequest FindCatalogItemRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      FindCatalogItemRequest = new FindCatalogItemRequest { };
    }

    public async Task CatalogItem_Given_ItemExists()
    {
      FindCatalogItemResponse findCatalogItemResponse = await Send(FindCatalogItemRequest);

      ValidateFindCatalogItemResponse(findCatalogItemResponse);
    }

    private void ValidateFindCatalogItemResponse(FindCatalogItemResponse aFindCatalogItemResponse)
    {
      aFindCatalogItemResponse.RequestId.ShouldBe(FindCatalogItemRequest.Id);
      aFindCatalogItemResponse.RequestId.Should().Be(FindCatalogItemRequest.Id);

    }

  }
}