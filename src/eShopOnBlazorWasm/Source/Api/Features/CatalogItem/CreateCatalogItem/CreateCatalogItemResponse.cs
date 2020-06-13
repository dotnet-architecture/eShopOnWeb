namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class CreateCatalogItemResponse : BaseResponse
  {

    /// <summary>
    /// The created Item
    /// </summary>
    public CatalogItemDto CatalogItem { get; set; }

    public CreateCatalogItemResponse() { }

    public CreateCatalogItemResponse(Guid aRequestId): base(aRequestId) { }
  }
}
