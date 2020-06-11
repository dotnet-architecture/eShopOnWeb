namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class GetCatalogBrandsResponse : BaseResponse
  {
    public GetCatalogBrandsResponse() { }

    public GetCatalogBrandsResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
