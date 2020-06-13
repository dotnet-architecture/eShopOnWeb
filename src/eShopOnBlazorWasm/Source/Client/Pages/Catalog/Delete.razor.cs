namespace eShopOnBlazorWasm.Pages.Catalog
{
  using BlazorState.Features.Routing;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;
  using Microsoft.AspNetCore.Components;

  public partial class Delete: BaseComponent
  {
    public const string Route = "/Catalog/Delete/{EntityId}";

    public static string RouteFactory(int aEntityId) =>
      Route.Replace($"{{{nameof(EntityId)}}}", aEntityId.ToString(), System.StringComparison.OrdinalIgnoreCase);

    [Parameter] public string EntityId { get; set; }

    protected async Task ButtonClick() =>
      _ = await Mediator.Send(new RouteState.ChangeRouteAction { NewRoute = "/" });
  }
}
