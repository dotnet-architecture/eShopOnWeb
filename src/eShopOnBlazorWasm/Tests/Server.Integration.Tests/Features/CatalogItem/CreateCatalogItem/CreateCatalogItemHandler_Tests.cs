namespace CreateCatalogItemHandler
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
    private readonly CreateCatalogItemRequest CreateCatalogItemRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      CreateCatalogItemRequest = new CreateCatalogItemRequest { };
    }

    public async Task NewCatalogItem()
    {
      CreateCatalogItemResponse createCatalogItemResponse = await Send(CreateCatalogItemRequest);

      ValidateCreateCatalogItemResponse(createCatalogItemResponse);
    }

    private void ValidateCreateCatalogItemResponse(CreateCatalogItemResponse aCreateCatalogItemResponse)
    {
      aCreateCatalogItemResponse.RequestId.Should().Be(CreateCatalogItemRequest.RequestId);
    }

  }
}