namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  using eShopOnBlazorWasm.Features.Bases;

  public class DeleteCatalogItemResponse : BaseResponse
  {
    public DeleteCatalogItemResponse() { }

    public DeleteCatalogItemResponse(Guid aRequestId) : base(aRequestId) { }
  }
}
