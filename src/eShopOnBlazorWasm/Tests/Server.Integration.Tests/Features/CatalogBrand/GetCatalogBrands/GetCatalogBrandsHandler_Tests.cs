namespace GetCatalogBrandsHandler
{
  using eShopOnBlazorWasm.Features.CatalogBrands;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Text.Json;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly GetCatalogBrandsRequest GetCatalogBrandsRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogBrandsRequest = new GetCatalogBrandsRequest { };
    }

    public async Task AllCatalogBrands()
    {
      GetCatalogBrandsResponse getCatalogBrandsResponse = await Send(GetCatalogBrandsRequest);

      ValidateGetCatalogBrandsResponse(getCatalogBrandsResponse);
    }

    private void ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse aGetCatalogBrandsResponse)
    {
      aGetCatalogBrandsResponse.RequestId.Should().Be(GetCatalogBrandsRequest.Id);
    }

  }
}