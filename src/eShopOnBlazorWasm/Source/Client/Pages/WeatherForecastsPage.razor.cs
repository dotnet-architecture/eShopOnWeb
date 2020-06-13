namespace eShopOnBlazorWasm.Pages
{
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;
  using static eShopOnBlazorWasm.Features.WeatherForecasts.WeatherForecastsState;

  public partial class WeatherForecastsPage : BaseComponent
  {
    public const string Route = "/weatherforecasts";

    protected override async Task OnInitializedAsync() =>
      await Mediator.Send(new FetchWeatherForecastsAction());
  }
}
