namespace eShopOnBlazorWasm.Features.Catalogs
{
  using System;
  using System.Collections.Generic;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogBrandsResponse : BaseResponse
  {
    /// <summary>
    /// a default constructor is required for deserialization
    /// </summary>
    public GetCatalogBrandsResponse() { }

    public GetCatalogBrandsResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
