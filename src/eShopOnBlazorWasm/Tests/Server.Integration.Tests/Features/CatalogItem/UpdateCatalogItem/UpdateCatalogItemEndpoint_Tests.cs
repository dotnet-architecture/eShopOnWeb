namespace UpdateCatalogItemEndpoint
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
    private readonly UpdateCatalogItemRequest UpdateCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      UpdateCatalogItemRequest = new UpdateCatalogItemRequest { };
    }

    public async Task UpdatedCatalogItem()
    {
      UpdateCatalogItemResponse updateCatalogItemResponse =
        await GetJsonAsync<UpdateCatalogItemResponse>(UpdateCatalogItemRequest.RouteFactory);

      ValidateUpdateCatalogItemResponse(updateCatalogItemResponse);
    }

    private void ValidateUpdateCatalogItemResponse(UpdateCatalogItemResponse aUpdateCatalogItemResponse)
    {
      aUpdateCatalogItemResponse.RequestId.ShouldBe(UpdateCatalogItemRequest.RequestId);
      aUpdateCatalogItemResponse.RequestId.Should().Be(UpdateCatalogItemRequest.RequestId);
    }

  }
}