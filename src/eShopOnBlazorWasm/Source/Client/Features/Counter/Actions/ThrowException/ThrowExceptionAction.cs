namespace eShopOnBlazorWasm.Features.Counters
{
  using eShopOnBlazorWasm.Features.Bases;

  internal partial class CounterState
  {
    public class ThrowExceptionAction : BaseAction
    {
      public string Message { get; set; }
    }
  }
}
