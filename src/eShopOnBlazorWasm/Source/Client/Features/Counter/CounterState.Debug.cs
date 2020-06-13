namespace eShopOnBlazorWasm.Features.Counters
{
  using BlazorState;
  using Microsoft.JSInterop;
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  internal partial class CounterState : State<CounterState>
  {
    public override CounterState Hydrate(IDictionary<string, object> aKeyValuePairs)
    {
      var counterState = new CounterState()
      {
        Count = Convert.ToInt32(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Count))].ToString()),
        Guid = new System.Guid(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Guid))].ToString()),
      };

      return counterState;
    }

    /// <summary>
    /// Use in Tests ONLY, to initialize the State
    /// </summary>
    /// <param name="aCount"></param>
    public void Initialize(int aCount)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      Count = aCount;
    }
  }
}
