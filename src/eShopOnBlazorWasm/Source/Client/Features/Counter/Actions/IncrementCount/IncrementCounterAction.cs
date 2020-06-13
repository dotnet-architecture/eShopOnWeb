namespace eShopOnBlazorWasm.Features.Counters
{
  using eShopOnBlazorWasm.Features.Bases;

  internal partial class CounterState
  {
    public class IncrementCounterAction : BaseAction
    {
      public int Amount { get; set; }
    }
  }
}
