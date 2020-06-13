namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using eShopOnBlazorWasm.Features.Catalogs;
  using System.Collections.Generic;
  using System.Text.Json.Serialization;

  internal partial class CatalogTypeState : State<CatalogTypeState>
  {
    private Dictionary<int, CatalogTypeDto> _CatalogTypes;

    [JsonIgnore]
    public IReadOnlyDictionary<int, CatalogTypeDto> CatalogTypes => _CatalogTypes;

    public CatalogTypeState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogTypes = new Dictionary<int, CatalogTypeDto>();
  }
}
