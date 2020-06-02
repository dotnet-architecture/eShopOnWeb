namespace eShopOnBlazorWasm.Features.EventStreams
{
  using BlazorState;
  using System.Collections.Generic;
  using System.Reflection;

  internal partial class EventStreamState : State<EventStreamState>
  {
    /// <summary>
    /// Use in Tests ONLY, to initialize the State
    /// </summary>
    /// <param name="aEvents"></param>
    public void Initialize(List<string> aEvents)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      _Events = aEvents;
    }
  }
}
