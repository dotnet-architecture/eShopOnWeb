namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  using eShopOnBlazorWasm.Features.Bases;
  using eShopOnBlazorWasm.Features.CatalogItem;

  public class GetCatalogItemResponse : BaseResponse
  {
    public CatalogItemDto CatalogItem { get; set; }

    public GetCatalogItemResponse() { }

    public GetCatalogItemResponse(Guid aRequestId) : base(aRequestId) { }
  }
}
