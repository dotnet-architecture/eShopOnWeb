namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.Json.Serialization;

  internal partial class CatalogTypeState : State<CatalogTypeState>
  {
    private Dictionary<int, CatalogTypeDto> _CatalogTypes;

    [JsonIgnore]
    public IReadOnlyDictionary<int, CatalogTypeDto> CatalogTypes => _CatalogTypes;

    public IReadOnlyList<CatalogTypeDto> CatalogTypesAsList => _CatalogTypes.Values.ToList();

    public CatalogTypeState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogTypes = new Dictionary<int, CatalogTypeDto>();
  }
}
