namespace FindCatalogItemHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;
  using eShopOnBlazorWasm.Features.CatalogItems;

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
      aFindCatalogItemResponse.CorrelationId.Should().Be(FindCatalogItemRequest.CorrelationId);
    }

  }
}