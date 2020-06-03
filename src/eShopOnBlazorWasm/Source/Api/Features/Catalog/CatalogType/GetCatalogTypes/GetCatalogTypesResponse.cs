namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class GetCatalogTypesResponse : BaseResponse
  {
    public GetCatalogTypesResponse() { }

    public GetCatalogTypesResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
