namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class CatalogBrandState : State<CatalogBrandState>
  {

    private Dictionary<int, CatalogBrandDto> _CatalogBrands;

    public IReadOnlyDictionary<int, CatalogBrandDto> CatalogBrands => _CatalogBrands;

    public CatalogBrandState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogBrands = new Dictionary<int, CatalogBrandDto>();
  }
}
