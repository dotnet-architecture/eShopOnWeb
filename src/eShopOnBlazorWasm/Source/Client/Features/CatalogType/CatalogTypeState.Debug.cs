namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using System.Collections.Generic;
  using System.Linq;
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
    public void Initialize(List<CatalogTypeDto> aCatalogTypes)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());

      _CatalogTypes = aCatalogTypes.ToDictionary( aCatalogType => aCatalogType.Id, aCatalogType => aCatalogType);
    }
  }
}
