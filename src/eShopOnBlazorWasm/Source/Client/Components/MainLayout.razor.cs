namespace eShopOnBlazorWasm.Components
{
  using BlazorState.Services;
  using Microsoft.AspNetCore.Components;

  public partial class MainLayout : LayoutComponentBase
  {
    protected const string HeadingHeight = "52px";
    [Inject] public BlazorHostingLocation BlazorHostingLocation { get; set; }
  }
}
