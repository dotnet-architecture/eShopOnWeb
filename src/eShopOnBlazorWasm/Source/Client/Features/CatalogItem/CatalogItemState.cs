namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using BlazorState;
  using System.Collections.Generic;

  internal partial class CatalogItemState : State<CatalogItemState>
  {
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }

    private List<CatalogItemDto> _CatalogItems;

    public IReadOnlyList<CatalogItemDto> CatalogItems => _CatalogItems.AsReadOnly();

    public CatalogItemState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize() => _CatalogItems = new List<CatalogItemDto>();
  }
}
