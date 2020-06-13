namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemResponse : BaseResponse
  {
    public CatalogItemDto CatalogItem { get; set; }

    public GetCatalogItemResponse() { }

    public GetCatalogItemResponse(Guid aRequestId) : base(aRequestId) { }
  }
}
