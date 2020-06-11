namespace eShopOnBlazorWasm.Features.Catalogs
{
  using System;
  using System.Collections.Generic;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogTypesResponse : BaseResponse
  {
    /// <summary>
    /// a default constructor is required for deserialization
    /// </summary>
    public GetCatalogTypesResponse() { }

    public GetCatalogTypesResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
