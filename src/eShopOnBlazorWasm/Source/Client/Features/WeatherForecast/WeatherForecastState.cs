namespace eShopOnBlazorWasm.Features.WeatherForecasts
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class WeatherForecastsState : State<WeatherForecastsState>
  {
    private List<WeatherForecastDto> _WeatherForecasts;

    public IReadOnlyList<WeatherForecastDto> WeatherForecasts => _WeatherForecasts.AsReadOnly();

    public WeatherForecastsState()
    {
      _WeatherForecasts = new List<WeatherForecastDto>();
    }

    public override void Initialize() { }
  }
}
