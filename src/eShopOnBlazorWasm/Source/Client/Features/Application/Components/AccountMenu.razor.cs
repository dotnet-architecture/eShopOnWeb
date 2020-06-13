namespace eShopOnBlazorWasm.Features.Applications.Components
{
  public partial class AccountMenu
  {
    protected bool Show { get; set; }

    protected string ShowCssClass => Show ? "show" : null;

    protected void ButtonClick() => Show = !Show;
  }
}
