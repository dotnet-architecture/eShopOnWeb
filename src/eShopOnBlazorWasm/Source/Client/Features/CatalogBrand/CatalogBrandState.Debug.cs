namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using BlazorState;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  internal partial class CatalogBrandState : State<CatalogBrandState>
  {
    public override CatalogBrandState Hydrate(IDictionary<string, object> aKeyValuePairs)
    {
      var catalogBrandState = new CatalogBrandState()
      {
        //Count = Convert.ToInt32(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(CatalogItems))].ToString()),
        //Guid = new System.Guid(aKeyValuePairs[CamelCase.MemberNameToCamelCase(nameof(Guid))].ToString()),
      };

      return catalogBrandState;
    }

    /// <summary>
    /// Use in Tests ONLY, to initialize the State
    /// </summary>
    /// <param name="aCount"></param>
    public void Initialize(List<CatalogBrandDto> aCatalogItems)
    {
      ThrowIfNotTestAssembly(Assembly.GetCallingAssembly());
      _CatalogBrands = aCatalogItems
        .ToDictionary(aCatalogBrand => aCatalogBrand.Id, aCatalogBrand => aCatalogBrand);
    }
  }
}
