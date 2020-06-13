namespace eShopOnBlazorWasm.Pages.Catalog
{
  using BlazorState.Features.Routing;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public partial class Delete: BaseComponent
  {
    public const string Route = "/Catalog/Delete";

    protected async Task ButtonClick() =>
      _ = await Mediator.Send(new RouteState.ChangeRouteAction { NewRoute = "/" });
  }
}
