namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using eShopOnBlazorWasm.Features.Catalogs;
  using System.Collections.Generic;

  internal partial class CatalogTypeState : State<CatalogTypeState>
  {

    private Dictionary<int, CatalogTypeDto> _CatalogTypes;

    public IReadOnlyDictionary<int, CatalogTypeDto> CatalogItems => _CatalogTypes;

    public CatalogTypeState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogTypes = new Dictionary<int, CatalogTypeDto>();
  }
}
