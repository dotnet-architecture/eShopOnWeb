namespace eShopOnBlazorWasm.Pages.Catalog
{
  using BlazorState.Features.Routing;
  using eShopOnBlazorWasm.Features.Bases;
  using static eShopOnBlazorWasm.Features.CatalogBrands.CatalogBrandState;
  using static eShopOnBlazorWasm.Features.CatalogItems.CatalogItemState;
  using static eShopOnBlazorWasm.Features.CatalogTypes.CatalogTypeState;
  using System.Threading.Tasks;

  public partial class Index: BaseComponent
  {
    public const string Route = "/Catalog";

    protected async Task ButtonClick() =>
      _ = await Mediator.Send(new RouteState.ChangeRouteAction { NewRoute = "/" });

    protected override async Task OnInitializedAsync()
    {
      await Mediator.Send(new FetchCatalogTypesAction());
      await Mediator.Send(new FetchCatalogBrandsAction());
      await Mediator.Send(new FetchCatalogItemsAction());
    }
  }
}
