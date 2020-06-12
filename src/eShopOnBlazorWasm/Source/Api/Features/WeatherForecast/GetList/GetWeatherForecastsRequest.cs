namespace eShopOnBlazorWasm.Features.WeatherForecasts
{
  using MediatR;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetWeatherForecastsRequest : BaseApiRequest, IRequest<GetWeatherForecastsResponse>
  {
    public const string Route = "api/weatherForecasts";

    /// <summary>
    /// The Number of days of forecasts to get
    /// </summary>
    /// <example>5</example>
    public int Days { get; set; }

    internal override string RouteFactory => $"{Route}?{nameof(Days)}={Days}&{nameof(RequestId)}={RequestId}";
  }
}
