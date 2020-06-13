namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using BlazorState;
  using eShopOnBlazorWasm.Features.Catalogs;
  using System.Collections.Generic;

  internal partial class CatalogTypeState : State<CatalogTypeState>
  {

    private List<CatalogTypeDto> _CatalogTypes;

    public IReadOnlyList<CatalogTypeDto> CatalogItems => _CatalogTypes.AsReadOnly();

    public CatalogTypeState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogTypes = new List<CatalogTypeDto>();
  }
}
