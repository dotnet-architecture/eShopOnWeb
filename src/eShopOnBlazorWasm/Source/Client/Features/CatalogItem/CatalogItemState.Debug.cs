namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using BlazorState;
  using Microsoft.JSInterop;
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  
  internal partial class CatalogItemState : State<CatalogItemState>
  {
    public override CatalogItemState Hydrate(IDictionary<string, object> aKeyValuePairs)
    {
      var catalogItemState = new CatalogItemState()
      {
        //Count = Convert.ToInt32(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(CatalogItems))].ToString()),
        //Guid = new System.Guid(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Guid))].ToString()),
      };

      return catalogItemState;
    }

    /// <summary>
    /// Use in Tests ONLY, to initialize the State
    /// </summary>
    /// <param name="aCount"></param>
    public void Initialize(List<CatalogItemDto> aCatalogItems)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      _CatalogItems = aCatalogItems;
    }
  }
}
