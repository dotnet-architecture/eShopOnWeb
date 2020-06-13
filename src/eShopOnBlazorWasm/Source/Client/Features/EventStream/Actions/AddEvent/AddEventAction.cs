namespace eShopOnBlazorWasm.Features.EventStreams
{
  using eShopOnBlazorWasm.Features.Bases;

  internal partial class EventStreamState
  {
    public class AddEventAction : BaseAction
    {
      public string Message { get; set; }
    }
  }
}
