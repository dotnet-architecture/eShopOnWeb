namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class GetCatalogItemHandler : IRequestHandler<GetCatalogItemRequest, GetCatalogItemResponse>
  {

    public async Task<GetCatalogItemResponse> Handle
    (
      GetCatalogItemRequest aGetCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new GetCatalogItemResponse(aGetCatalogItemRequest.RequestId);

      return await Task.Run(() => response);
    }
  }
}
