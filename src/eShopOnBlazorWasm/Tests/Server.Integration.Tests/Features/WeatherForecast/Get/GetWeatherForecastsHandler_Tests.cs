namespace GetWeatherForecastsHandler
{
  using Shouldly;
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure;
  using eShopOnBlazorWasm.Features.WeatherForecasts;
  using eShopOnBlazorWasm.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly GetWeatherForecastsRequest GetWeatherForecastsRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      GetWeatherForecastsRequest = new GetWeatherForecastsRequest { Days = 10 };
    }

    public async Task _10WeatherForecasts_Given_10DaysRequested()
    {
      GetWeatherForecastsResponse getWeatherForecastsResponse = await Send(GetWeatherForecastsRequest);

      ValidateGetWeatherForecastsResponse(getWeatherForecastsResponse);
    }

    private void ValidateGetWeatherForecastsResponse(GetWeatherForecastsResponse aGetWeatherForecastsResponse)
    {
      aGetWeatherForecastsResponse.RequestId.ShouldBe(GetWeatherForecastsRequest.RequestId);
      aGetWeatherForecastsResponse.RequestId.Should().Be(GetWeatherForecastsRequest.RequestId);
      aGetWeatherForecastsResponse.WeatherForecasts.Count.ShouldBe(GetWeatherForecastsRequest.Days);
      aGetWeatherForecastsResponse.WeatherForecasts.Count.Should().Be(GetWeatherForecastsRequest.Days);
    }

  }
}