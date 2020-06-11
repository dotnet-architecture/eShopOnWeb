namespace GetCatalogTypesEndpoint
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
    private readonly GetCatalogTypesRequest GetCatalogTypesRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogTypesRequest = new GetCatalogTypesRequest { Days = 10 };
    }

    public async Task GetCatalogTypesResponse()
    {
      GetCatalogTypesResponse GetCatalogTypesResponse =
        await GetJsonAsync<GetCatalogTypesResponse>(GetCatalogTypesRequest.RouteFactory);

      ValidateGetCatalogTypesResponse(GetCatalogTypesResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value
      GetCatalogTypesRequest.Days = -1;

      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(GetCatalogTypesRequest.RouteFactory);

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(nameof(GetCatalogTypesRequest.Days));
    }

    private void ValidateGetCatalogTypesResponse(GetCatalogTypesResponse aGetCatalogTypesResponse)
    {
      aGetCatalogTypesResponse.RequestId.Should().Be(GetCatalogTypesRequest.Id);
      // check Other properties here
    }
  }
}