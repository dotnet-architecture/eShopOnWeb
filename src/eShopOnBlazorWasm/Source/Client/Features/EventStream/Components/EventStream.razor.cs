namespace eShopOnBlazorWasm.Features.EventStreams
{
  using System.Collections.Generic;

  public partial class EventStream
  {
    public IReadOnlyList<string> Events => EventStreamState.Events;
  }
}
