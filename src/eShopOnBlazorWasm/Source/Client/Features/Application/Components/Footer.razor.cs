namespace eShopOnBlazorWasm.Features.Applications.Components
{
  using eShopOnBlazorWasm.Features.Bases;

  public partial class Footer: BaseComponent
  {
    protected string Version => ApplicationState.Version;
  }
}
