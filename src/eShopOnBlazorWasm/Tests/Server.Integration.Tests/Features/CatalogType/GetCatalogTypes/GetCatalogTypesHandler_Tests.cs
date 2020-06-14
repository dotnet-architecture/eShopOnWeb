namespace GetCatalogTypesHandler
{
  using Shouldly;
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;
  using eShopOnBlazorWasm.Features.CatalogTypes;

  public class Handle_Returns : BaseTest
  {
    private readonly GetCatalogTypesRequest GetCatalogTypesRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetCatalogTypesRequest = new GetCatalogTypesRequest { };
    }

    public async Task _10WeatherForecasts_Given_10DaysRequested()
    {
      GetCatalogTypesResponse getCatalogTypesResponse = await Send(GetCatalogTypesRequest);

      ValidateGetCatalogTypesResponse(getCatalogTypesResponse);
    }

    private void ValidateGetCatalogTypesResponse(GetCatalogTypesResponse aGetCatalogTypesResponse)
    {
      aGetCatalogTypesResponse.CorrelationId.ShouldBe(GetCatalogTypesRequest.CorrelationId);
      aGetCatalogTypesResponse.CorrelationId.Should().Be(GetCatalogTypesRequest.CorrelationId);
    }

  }
}