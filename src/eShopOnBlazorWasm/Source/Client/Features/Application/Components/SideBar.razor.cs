namespace eShopOnBlazorWasm.Features.Applications.Components
{
  using eShopOnBlazorWasm.Features.Bases;

  public partial class SideBar: BaseComponent
  {
    protected string NavMenuCssClass => ApplicationState.IsMenuExpanded ? null : "collapse";
  }
}
