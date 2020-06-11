namespace eShopOnBlazorWasm.Features.Catalog
{
  using MediatR;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class CreateCatalogItemHandler : IRequestHandler<CreateCatalogItemRequest, CreateCatalogItemResponse>
  {
    public async Task<CreateCatalogItemResponse> Handle
    (
      CreateCatalogItemRequest aCreateCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new CreateCatalogItemResponse(aCreateCatalogItemRequest.Id);

      return await Task.Run(() => response);
    }
  }
}
