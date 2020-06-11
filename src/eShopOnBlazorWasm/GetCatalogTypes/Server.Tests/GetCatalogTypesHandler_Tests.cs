namespace GetCatalogTypesHandler
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
    private readonly GetCatalogTypesRequest GetCatalogTypesRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogTypesRequest = new GetCatalogTypesRequest { Days = 10 };
    }

    public async Task GetCatalogTypesResponse()
    {
      GetCatalogTypesResponse GetCatalogTypesResponse = await Send(GetCatalogTypesRequest);

      ValidateGetCatalogTypesResponse(GetCatalogTypesResponse);
    }

    private void ValidateGetCatalogTypesResponse(GetCatalogTypesResponse aGetCatalogTypesResponse)
    {
      aGetCatalogTypesResponse.RequestId.Should().Be(GetCatalogTypesRequest.Id);
      // check Other properties here
    }

  }
}