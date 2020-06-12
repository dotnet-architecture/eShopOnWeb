namespace GetCatalogTypesEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using eShopOnBlazorWasm.Features.Catalogs;

  public class Returns : BaseTest
  {
    private readonly GetCatalogTypesRequest GetCatalogTypesRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogTypesRequest = new GetCatalogTypesRequest { };
    }

    public async Task AllCatalogTypes()
    {
      GetCatalogTypesResponse getCatalogTypesResponse =
        await GetJsonAsync<GetCatalogTypesResponse>(GetCatalogTypesRequest.RouteFactory);

      ValidateGetCatalogTypesResponse(getCatalogTypesResponse);
    }

    private void ValidateGetCatalogTypesResponse(GetCatalogTypesResponse aGetCatalogTypesResponse) =>
      aGetCatalogTypesResponse.RequestId.Should().Be(GetCatalogTypesRequest.RequestId);
  }
}