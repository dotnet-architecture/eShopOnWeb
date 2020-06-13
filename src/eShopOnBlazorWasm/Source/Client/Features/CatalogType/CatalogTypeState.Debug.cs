namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using eShopOnBlazorWasm.Features.Catalogs;
  using Microsoft.JSInterop;
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  
  internal partial class CatalogTypeState : State<CatalogTypeState>
  {
    public override CatalogTypeState Hydrate(IDictionary<string, object> aKeyValuePairs)
    {
      var catalogTypeState = new CatalogTypeState()
      {
        //Count = Convert.ToInt32(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(CatalogItems))].ToString()),
        //Guid = new System.Guid(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Guid))].ToString()),
      };

      return catalogTypeState;
    }

    /// <summary>
    /// Use in Tests ONLY, to initialize the State
    /// </summary>
    /// <param name="aCount"></param>
    public void Initialize(List<CatalogTypeDto> aCatalogItems)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      _CatalogTypes = aCatalogItems;
    }
  }
}
