namespace UpdateCatalogItemHandler
{
  using Shouldly;
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;
  using eShopOnBlazorWasm.Features.CatalogItems;

  public class Handle_Returns : BaseTest
  {
    private readonly UpdateCatalogItemRequest UpdateCatalogItemRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      UpdateCatalogItemRequest = new UpdateCatalogItemRequest { CatalogItemId = 10 };
    }

    public async Task UpdatedCatalogItem()
    {
      UpdateCatalogItemResponse updateCatalogItemResponse = await Send(UpdateCatalogItemRequest);

      ValidateUpdateCatalogItemResponse(updateCatalogItemResponse);
    }

    private void ValidateUpdateCatalogItemResponse(UpdateCatalogItemResponse aUpdateCatalogItemResponse)
    {
      aUpdateCatalogItemResponse.CorrelationId.Should().Be(UpdateCatalogItemRequest.CorrelationId);
      
    }

  }
}