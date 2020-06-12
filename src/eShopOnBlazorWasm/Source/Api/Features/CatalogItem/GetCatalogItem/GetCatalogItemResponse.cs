namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  using System.Collections.Generic;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemResponse : BaseResponse
  {
    public GetCatalogItemResponse() { }

    public GetCatalogItemResponse(Guid aRequestId) : base(aRequestId) { }
  }
}
