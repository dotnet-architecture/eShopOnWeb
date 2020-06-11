namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class RemoveCatalogItemHandler : IRequestHandler<RemoveCatalogItemRequest, RemoveCatalogItemResponse>
  {
    public async Task<RemoveCatalogItemResponse> Handle
    (
      RemoveCatalogItemRequest aRemoveCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new RemoveCatalogItemResponse(aRemoveCatalogItemRequest.Id);

      return await Task.Run(() => response);
    }
  }
}
