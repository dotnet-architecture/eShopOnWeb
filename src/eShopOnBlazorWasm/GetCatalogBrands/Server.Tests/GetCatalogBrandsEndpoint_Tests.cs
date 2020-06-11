namespace GetCatalogBrandsEndpoint
{
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using System.Net;
  using System.Net.Http;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Catalogs;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;

  public class Returns : BaseTest
  {
    private readonly GetCatalogBrandsRequest GetCatalogBrandsRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogBrandsRequest = new GetCatalogBrandsRequest { Days = 10 };
    }

    public async Task GetCatalogBrandsResponse()
    {
      GetCatalogBrandsResponse GetCatalogBrandsResponse =
        await GetJsonAsync<GetCatalogBrandsResponse>(GetCatalogBrandsRequest.RouteFactory);

      ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      GetCatalogBrandsRequest.Days = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(GetCatalogBrandsRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(GetCatalogBrandsRequest.Days));
    }

    private void ValidateGetCatalogBrandsResponse(GetCatalogBrandsResponse aGetCatalogBrandsResponse)
    {
      aGetCatalogBrandsResponse.RequestId.Should().Be(GetCatalogBrandsRequest.Id);
      // check Other properties here
    }
  }
}