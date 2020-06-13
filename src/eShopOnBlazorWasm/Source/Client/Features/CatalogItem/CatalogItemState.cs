namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class CatalogItemState : State<CatalogItemState>
  {

    private List<CatalogItemDto> _CatalogItems;

    public IReadOnlyList<CatalogItemDto> CatalogItems => _CatalogItems.AsReadOnly();

    public CatalogItemState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogItems = new List<CatalogItemDto>();
  }
}
