namespace eShopOnBlazorWasm.Features.WeatherForecasts
{
  using System;
  using System.Collections.Generic;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetWeatherForecastsResponse : BaseResponse
  {
    /// <summary>
    /// The collection of forecasts requested
    /// </summary>
    public List<WeatherForecastDto> WeatherForecasts { get; set; }

    /// <summary>
    /// a default constructor is required for deserialization
    /// </summary>
    public GetWeatherForecastsResponse() 
    { 
      WeatherForecasts = new List<WeatherForecastDto>();
    }

    public GetWeatherForecastsResponse(Guid aRequestId): this()
    {
      RequestId = aRequestId;
    }
  }
}
