namespace eShopOnBlazorWasm.Features.EventStreams
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class EventStreamState : State<EventStreamState>
  {
    public List<string> _Events { get; set; }
    public IReadOnlyList<string> Events => _Events.AsReadOnly();

    public EventStreamState()
    {
      _Events = new List<string>();
    }

    public override void Initialize() { }
  }
}
