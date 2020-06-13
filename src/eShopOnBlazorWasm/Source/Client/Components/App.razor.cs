namespace eShopOnBlazorWasm.Components
{
  using BlazorState.Features.JavaScriptInterop;
  using BlazorState.Features.Routing;
  using BlazorState.Pipeline.ReduxDevTools;
  using Microsoft.AspNetCore.Components;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.ClientLoaders;

  public partial class App : ComponentBase
  {
    [Inject] private ClientLoader ClientLoader { get; set; }
    [Inject] private JsonRequestHandler JsonRequestHandler { get; set; }
    [Inject] private RouteManager RouteManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool aFirstRender)
    {
      await JsonRequestHandler.InitAsync();
      await ClientLoader.InitAsync();
    }
  }
}
