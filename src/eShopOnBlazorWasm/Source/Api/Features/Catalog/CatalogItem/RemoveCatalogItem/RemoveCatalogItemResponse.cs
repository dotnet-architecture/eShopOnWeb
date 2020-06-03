namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class RemoveCatalogItemResponse : BaseResponse
  {
    public RemoveCatalogItemResponse() { }

    public RemoveCatalogItemResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
