namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public class CreateCatalogItemResponse : BaseResponse
  {
    public CreateCatalogItemResponse(Guid aRequestId)
    {
      RequestId = aRequestId;
    }
  }
}
