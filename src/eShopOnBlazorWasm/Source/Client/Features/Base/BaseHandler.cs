namespace eShopOnBlazorWasm.Features.Bases
{
  using BlazorState;
  using eShopOnBlazorWasm.Features.Applications;
  using eShopOnBlazorWasm.Features.CatalogBrands;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using eShopOnBlazorWasm.Features.CatalogTypes;
  using eShopOnBlazorWasm.Features.Counters;
  using eShopOnBlazorWasm.Features.EventStreams;
  using eShopOnBlazorWasm.Features.WeatherForecasts;

  /// <summary>
  /// Base Handler that makes it easy to access state
  /// </summary>
  /// <typeparam name="TAction"></typeparam>
  internal abstract class BaseHandler<TAction> : ActionHandler<TAction>
    where TAction : IAction
  {
    protected ApplicationState ApplicationState => Store.GetState<ApplicationState>();

    protected CatalogBrandState CatalogBrandState => Store.GetState<CatalogBrandState>();

    protected CatalogItemState CatalogItemState => Store.GetState<CatalogItemState>();

    protected CatalogTypeState CatalogTypeState => Store.GetState<CatalogTypeState>();

    protected CounterState CounterState => Store.GetState<CounterState>();

    protected EventStreamState EventStreamState => Store.GetState<EventStreamState>();

    protected WeatherForecastsState WeatherForecastsState => Store.GetState<WeatherForecastsState>();

    public BaseHandler(IStore aStore) : base(aStore) { }
  }
}
