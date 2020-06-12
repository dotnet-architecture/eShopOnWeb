namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class UpdateCatalogItemHandler : IRequestHandler<UpdateCatalogItemRequest, UpdateCatalogItemResponse>
  {
    public async Task<UpdateCatalogItemResponse> Handle
    (
      UpdateCatalogItemRequest aUpdateCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new UpdateCatalogItemResponse(aUpdateCatalogItemRequest.RequestId);

      return await Task.Run(() => response);
    }
  }
}
