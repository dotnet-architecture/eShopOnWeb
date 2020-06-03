namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class UpdateCatalogItemResponse : BaseResponse
  {
    public UpdateCatalogItemResponse() { }

    public UpdateCatalogItemResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
