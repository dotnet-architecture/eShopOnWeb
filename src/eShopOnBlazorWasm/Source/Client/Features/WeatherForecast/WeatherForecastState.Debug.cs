namespace eShopOnBlazorWasm.Features.WeatherForecasts
{
  using BlazorState;
  using Dawn;
  using Microsoft.JSInterop;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text.Json;

  internal partial class WeatherForecastsState : State<WeatherForecastsState>
  {
    public override WeatherForecastsState Hydrate(IDictionary<string, object> aKeyValuePairs)
    {
      string json = aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(WeatherForecasts))].ToString();

      var newWeatherForecastsState = new WeatherForecastsState()
      {
        _WeatherForecasts = JsonSerializer.Deserialize<List<WeatherForecastDto>>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        Guid = new System.Guid(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Guid))].ToString()),
      };

      return newWeatherForecastsState;
    }

    internal void Initialize(List<WeatherForecastDto> aWeatherForecastList)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      _WeatherForecasts = Guard.Argument(aWeatherForecastList, nameof(aWeatherForecastList)).NotNull();
    }
  }
}
