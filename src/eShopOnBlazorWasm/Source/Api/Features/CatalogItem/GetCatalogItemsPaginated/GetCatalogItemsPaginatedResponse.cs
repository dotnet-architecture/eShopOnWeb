namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;
  using System.Collections.Generic;

  public class GetCatalogItemsPaginatedResponse : BaseResponse
  {
    public List<CatalogItemDto> CatalogItems { get; set; }
    public GetCatalogItemsPaginatedResponse() { }

    public GetCatalogItemsPaginatedResponse(Guid aRequestId): base(aRequestId)
    {
      CatalogItems = new List<CatalogItemDto>();
    }
  }
}
