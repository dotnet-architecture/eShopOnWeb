namespace RemoveCatalogItemEndpoint
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
    private readonly RemoveCatalogItemRequest RemoveCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      RemoveCatalogItemRequest = new RemoveCatalogItemRequest { };
    }

    public async Task RemoveCatalogItemResponse()
    {
      RemoveCatalogItemResponse removeCatalogItemResponse =
        await GetJsonAsync<RemoveCatalogItemResponse>(RemoveCatalogItemRequest.RouteFactory);

      ValidateRemoveCatalogItemResponse(removeCatalogItemResponse);
    }

    private void ValidateRemoveCatalogItemResponse(RemoveCatalogItemResponse aRemoveCatalogItemResponse)
    {
      aRemoveCatalogItemResponse.RequestId.ShouldBe(RemoveCatalogItemRequest.RequestId);
      aRemoveCatalogItemResponse.RequestId.Should().Be(RemoveCatalogItemRequest.RequestId);
    }

  }
}