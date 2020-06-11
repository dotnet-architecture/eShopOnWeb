namespace eShopOnBlazorWasm.Features.Catalogs
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class GetCatalogBrandsHandler : IRequestHandler<GetCatalogBrandsRequest, GetCatalogBrandsResponse>
  {

    public async Task<GetCatalogBrandsResponse> Handle
    (
      GetCatalogBrandsRequest aGetCatalogBrandsRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new GetCatalogBrandsResponse(aGetCatalogBrandsRequest.Id);

      return await Task.Run(() => response);
    }
  }
}
