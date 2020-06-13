namespace GetCatalogItemHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly GetCatalogItemRequest GetCatalogItemRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogItemRequest = new GetCatalogItemRequest { CatalogItemId = 10 };
    }

    public async Task GetCatalogItemResponse()
    {
      GetCatalogItemResponse getCatalogItemResponse = await Send(GetCatalogItemRequest);

      ValidateGetCatalogItemResponse(getCatalogItemResponse);
    }

    private void ValidateGetCatalogItemResponse(GetCatalogItemResponse aGetCatalogItemResponse)
    {
      aGetCatalogItemResponse.CorrelationId.Should().Be(GetCatalogItemRequest.CorrelationId);
      // check Other properties here
    }

  }
}