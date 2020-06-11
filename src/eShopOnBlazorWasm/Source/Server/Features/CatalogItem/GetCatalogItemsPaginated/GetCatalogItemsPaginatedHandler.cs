namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class GetCatalogItemsPaginatedHandler : IRequestHandler<GetCatalogItemsPaginatedRequest, GetCatalogItemsPaginatedResponse>
  {
    public async Task<GetCatalogItemsPaginatedResponse> Handle
    (
      GetCatalogItemsPaginatedRequest aGetCatalogItemsPaginatedRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new GetCatalogItemsPaginatedResponse(aGetCatalogItemsPaginatedRequest.Id);

      return await Task.Run(() => response);
    }
  }
}
