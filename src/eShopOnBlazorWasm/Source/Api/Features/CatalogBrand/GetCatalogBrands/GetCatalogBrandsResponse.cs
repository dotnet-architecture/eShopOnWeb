namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;
  using System.Collections.Generic;

  public class GetCatalogBrandsResponse : BaseResponse
  {
    /// <summary>
    /// Catalog Brands
    /// </summary>
    public List<CatalogBrandDto> CatalogBrands { get; set; }

    public GetCatalogBrandsResponse() { }

    public GetCatalogBrandsResponse(Guid aRequestId) : base(aRequestId)
    {
      CatalogBrands = new List<CatalogBrandDto>();
    }
  }
}
