namespace WeatherForecastsState
{
  using Shouldly;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.WeatherForecasts;
  using eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure;
  using static eShopOnBlazorWasm.Features.WeatherForecasts.WeatherForecastsState;

  public class FetchWeatherForecastsAction_Should : BaseTest
  {
    private WeatherForecastsState WeatherForecastsState => Store.GetState<WeatherForecastsState>();

    public FetchWeatherForecastsAction_Should(ClientHost aWebAssemblyHost) : base(aWebAssemblyHost) { }

    public async Task Update_WeatherForecastState_With_WeatherForecasts_From_Server()
    {
      var fetchWeatherForecastsRequest = new FetchWeatherForecastsAction();

      await Send(fetchWeatherForecastsRequest);

      WeatherForecastsState.WeatherForecasts.Count.ShouldBeGreaterThan(0);
    }
  }
}
