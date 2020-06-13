namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using AutoMapper;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Threading;
  using System;
  
  public class DeleteCatalogItemHandler : IRequestHandler<DeleteCatalogItemRequest, DeleteCatalogItemResponse>
  {

    public DeleteCatalogItemHandler(IAsyncRepository<CatalogItem> aCatalogItemRepository, IMapper aMapper)
    {
      CatalogItemRepository = aCatalogItemRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogItem> CatalogItemRepository { get; }
    public IMapper Mapper { get; }

    public async Task<DeleteCatalogItemResponse> Handle
    (
      DeleteCatalogItemRequest aDeleteCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      CatalogItem catalogItem = await CatalogItemRepository.GetByIdAsync(aDeleteCatalogItemRequest.CatalogItemId);
      await CatalogItemRepository.DeleteAsync(catalogItem);

      var response = new DeleteCatalogItemResponse(aDeleteCatalogItemRequest.CorrelationId);

      return await Task.Run(() => response);
    }
  }
}
