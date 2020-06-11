namespace GetCatalogBrandsEnpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Shouldly;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using eShopOnBlazorWasm.Features.CatalogBrands;

  public class Returns : BaseTest
  {
    private readonly GetCatalogBrandsRequest GetCatalogBrandsRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogBrandsRequest = new GetCatalogBrandsRequest { };
    }

    public async Task AllCatalogBrands()
    {
      GetCatalogBrandsResponse getCatalogBrandsResponse =
        await GetJsonAsync<GetCatalogBrandsResponse>(GetCatalogBrandsRequest.RouteFactory);

      ValidateGetCatalogBrandsResponse(getCatalogBrandsResponse);
    }

    private void ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse aGetCatalogBrandsResponse)
    {
      aGetCatalogBrandsResponse.RequestId.ShouldBe(GetCatalogBrandsRequest.Id);
      aGetCatalogBrandsResponse.RequestId.Should().Be(GetCatalogBrandsRequest.Id);
      //aGetCatalogBrandsResponse.CatalogBrands.Count.ShouldBe(???);
      //aGetCatalogBrandsResponse.CatalogBrands.Count.Should().Be(???);
    }

  }
}