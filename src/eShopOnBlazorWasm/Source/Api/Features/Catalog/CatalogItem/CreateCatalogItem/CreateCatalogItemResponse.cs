namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class CreateCatalogItemResponse : BaseResponse
  {
    public CreateCatalogItemResponse() { }

    public CreateCatalogItemResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
