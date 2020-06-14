namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;
  using System.Collections.Generic;

  public class GetCatalogTypesResponse : BaseResponse
  {
    /// <summary>
    /// Catalog Types
    /// </summary>
    public List<CatalogTypeDto> CatalogTypes { get; set; }

    public GetCatalogTypesResponse() { }

    public GetCatalogTypesResponse(Guid aRequestId) : base(aRequestId)
    {
      CatalogTypes = new List<CatalogTypeDto>();
    }
  }
}
