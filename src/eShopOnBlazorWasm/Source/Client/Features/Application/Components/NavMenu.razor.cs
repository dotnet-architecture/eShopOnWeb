namespace eShopOnBlazorWasm.Features.Applications.Components
{
  using Microsoft.AspNetCore.Components;
  using eShopOnBlazorWasm.Features.Bases;

  public partial class NavMenu : BaseComponent
  {
    protected bool CollapseNavMenu { get; set; }

    protected string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

    protected void ToggleNavMenu() => CollapseNavMenu = !CollapseNavMenu;
  }
}
