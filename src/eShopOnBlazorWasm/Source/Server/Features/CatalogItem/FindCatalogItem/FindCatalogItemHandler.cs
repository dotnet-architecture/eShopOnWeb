namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class FindCatalogItemHandler : IRequestHandler<FindCatalogItemRequest, FindCatalogItemResponse>
  {
    public async Task<FindCatalogItemResponse> Handle
    (
      FindCatalogItemRequest aFindCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new FindCatalogItemResponse(aFindCatalogItemRequest.RequestId);

      return await Task.Run(() => response);
    }
  }
}
