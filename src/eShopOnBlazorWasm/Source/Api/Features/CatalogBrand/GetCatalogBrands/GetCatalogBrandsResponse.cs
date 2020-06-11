namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;

  public class GetCatalogBrandsResponse : BaseResponse
  {
    public GetCatalogBrandsResponse() { }

    public GetCatalogBrandsResponse(Guid aRequestId) : this()
    {
      RequestId = aRequestId;
    }
  }
}
