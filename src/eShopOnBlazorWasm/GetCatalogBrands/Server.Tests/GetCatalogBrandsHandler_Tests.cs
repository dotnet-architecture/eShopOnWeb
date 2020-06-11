namespace GetCatalogBrandsHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Features.Catalogs;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly GetCatalogBrandsRequest GetCatalogBrandsRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogBrandsRequest = new GetCatalogBrandsRequest { Days = 10 };
    }

    public async Task GetCatalogBrandsResponse()
    {
      GetCatalogBrandsResponse GetCatalogBrandsResponse = await Send(GetCatalogBrandsRequest);

      ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse);
    }

    private void ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse aGetCatalogBrandsResponse)
    {
      aGetCatalogBrandsResponse.RequestId.Should().Be(GetCatalogBrandsRequest.Id);
      // check Other properties here
    }

  }
}