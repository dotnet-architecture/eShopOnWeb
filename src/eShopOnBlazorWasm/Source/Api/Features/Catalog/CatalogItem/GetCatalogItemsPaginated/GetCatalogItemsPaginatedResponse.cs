namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class GetCatalogItemsPaginatedResponse : BaseResponse
  {
    public GetCatalogItemsPaginatedResponse() { }

    public GetCatalogItemsPaginatedResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
