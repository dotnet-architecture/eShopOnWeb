namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class CatalogBrandState : State<CatalogBrandState>
  {

    private List<CatalogBrandDto> _CatalogBrands;

    public IReadOnlyList<CatalogBrandDto> CatalogBrands => _CatalogBrands.AsReadOnly();

    public CatalogBrandState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogBrands = new List<CatalogBrandDto>();
  }
}
