namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class FindCatalogItemResponse : BaseResponse
  {
    public FindCatalogItemResponse() { }

    public FindCatalogItemResponse(Guid aRequestId) : base(aRequestId)
    {

    }
  }
}
