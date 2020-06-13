namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using BlazorState;
  using System;
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
    public override void Initialize()
    {
      Console.WriteLine("Initialize CatalogItemState");
      PageIndex = 0;
      PageSize = 10;
      _CatalogItems = new List<CatalogItemDto>();
    }
  }
}
