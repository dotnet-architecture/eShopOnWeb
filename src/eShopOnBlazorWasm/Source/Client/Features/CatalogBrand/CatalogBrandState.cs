namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using BlazorState;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.Json.Serialization;

  internal partial class CatalogBrandState : State<CatalogBrandState>
  {

    private Dictionary<int, CatalogBrandDto> _CatalogBrands;

    [JsonIgnore]
    public IReadOnlyDictionary<int, CatalogBrandDto> CatalogBrands => _CatalogBrands;

    public IReadOnlyList<CatalogBrandDto> CatalogBrandsAsList => _CatalogBrands.Values.ToList();

    public CatalogBrandState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogBrands = new Dictionary<int, CatalogBrandDto>();
  }
}
