namespace eShopOnBlazorWasm.Features.Catalogs
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;
  using System.Collections.Generic;

  public class GetCatalogTypesResponse : BaseResponse
  {
    /// <summary>
    /// The collection of forecasts requested
    /// </summary>
    public List<CatalogTypeDto> CatalogTypes { get; set; }

    public GetCatalogTypesResponse() 
    {
      CatalogTypes = new List<CatalogTypeDto>();
    }

    public GetCatalogTypesResponse(Guid aRequestId):this()
    {
      RequestId = aRequestId;
    }
  }
}
