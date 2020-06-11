namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class FindCatalogItemResponse : BaseResponse
  {
    public FindCatalogItemResponse() { }

    public FindCatalogItemResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
