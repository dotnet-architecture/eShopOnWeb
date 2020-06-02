namespace eShopOnBlazorWasm.Components
{
  using eShopOnBlazorWasm.Features.Bases;
  using static eShopOnBlazorWasm.Features.Applications.ApplicationState;

  public partial class ResetButton:BaseComponent
  {
    internal void ButtonClick() => Mediator.Send(new ResetStoreAction());
  }
}
