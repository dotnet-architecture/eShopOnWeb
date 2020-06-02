namespace CreateCatalogItemEndpoint
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
    private readonly CreateCatalogItemRequest CreateCatalogItemRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      CreateCatalogItemRequest = new CreateCatalogItemRequest { };
    }

    public async Task NewCatalogItem()
    {
      CreateCatalogItemResponse createCatalogItemResponse =
        await GetJsonAsync<CreateCatalogItemResponse>(CreateCatalogItemRequest.RouteFactory);

      ValidateCreateCatalogItemResponse(createCatalogItemResponse);
    }

    private void ValidateCreateCatalogItemResponse(CreateCatalogItemResponse aCreateCatalogItemResponse)
    {
      aCreateCatalogItemResponse.RequestId.ShouldBe(CreateCatalogItemRequest.Id);
      aCreateCatalogItemResponse.RequestId.Should().Be(CreateCatalogItemRequest.Id);
    }

  }
}