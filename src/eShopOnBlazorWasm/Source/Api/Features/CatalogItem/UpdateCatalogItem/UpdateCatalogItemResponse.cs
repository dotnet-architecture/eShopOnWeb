namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class UpdateCatalogItemResponse : BaseResponse
  {

    /// <summary>
    /// The updated Item
    /// </summary>
    public CatalogItemDto CatalogItem { get; set; }

    public UpdateCatalogItemResponse() { }

    public UpdateCatalogItemResponse(Guid aRequestId) : base(aRequestId) { }
  }
}
