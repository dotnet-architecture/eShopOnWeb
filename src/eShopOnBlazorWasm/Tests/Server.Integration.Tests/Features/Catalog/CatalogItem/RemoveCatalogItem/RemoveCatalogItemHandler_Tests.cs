namespace RemoveCatalogItemHandler
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
    private readonly RemoveCatalogItemRequest RemoveCatalogItemRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      RemoveCatalogItemRequest = new RemoveCatalogItemRequest { };
    }

    public async Task RemoveCatalogItemResponse()
    {
      RemoveCatalogItemResponse removeCatalogItemResponse = await Send(RemoveCatalogItemRequest);

      ValidateRemoveCatalogItemResponse(removeCatalogItemResponse);
    }

    private void ValidateRemoveCatalogItemResponse(RemoveCatalogItemResponse aRemoveCatalogItemResponse)
    {
      aRemoveCatalogItemResponse.RequestId.ShouldBe(RemoveCatalogItemRequest.Id);
      aRemoveCatalogItemResponse.RequestId.Should().Be(RemoveCatalogItemRequest.Id);
    }

  }
}