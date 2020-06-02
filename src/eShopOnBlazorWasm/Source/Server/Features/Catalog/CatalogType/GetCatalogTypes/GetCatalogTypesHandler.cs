namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class GetCatalogTypesHandler : IRequestHandler<GetCatalogTypesRequest, GetCatalogTypesResponse>
  {
    public async Task<GetCatalogTypesResponse> Handle
    (
      GetCatalogTypesRequest aGetCatalogTypesRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new GetCatalogTypesResponse(aGetCatalogTypesRequest.Id);

      return await Task.Run(() => response);
    }
  }
}
