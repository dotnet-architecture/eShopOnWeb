namespace eShopOnBlazorWasm.Features.Applications.Components
{
  using static eShopOnBlazorWasm.Features.Applications.ApplicationState;

  public partial class NavBar
  {
    protected string NavMenuCssClass => ApplicationState.IsMenuExpanded ? null : "collapse";

    protected async void ToggleNavMenu() => await Mediator.Send(new ToggleMenuAction());
  }
}
