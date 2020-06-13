namespace eShopOnBlazorWasm.Features.Catalog
{
  using AutoMapper;
  using eShopOnBlazorWasm.Features.CatalogItems;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Threading;
  using System;

  public class UpdateCatalogItemHandler : IRequestHandler<UpdateCatalogItemRequest, UpdateCatalogItemResponse>
  {
    public UpdateCatalogItemHandler(IAsyncRepository<CatalogItem> aCatalogItemRepository, IMapper aMapper)
    {
      CatalogItemRepository = aCatalogItemRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogItem> CatalogItemRepository { get; }
    public IMapper Mapper { get; }

    public async Task<UpdateCatalogItemResponse> Handle
    (
      UpdateCatalogItemRequest aUpdateCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      CatalogItem catalogItem = await CatalogItemRepository.GetByIdAsync(aUpdateCatalogItemRequest.CatalogItemId);
      _ = Mapper.Map<UpdateCatalogItemRequest, CatalogItem>(aUpdateCatalogItemRequest, catalogItem);
      await CatalogItemRepository.UpdateAsync(catalogItem);

      var response = new UpdateCatalogItemResponse(aUpdateCatalogItemRequest.RequestId)
      {
        CatalogItem = Mapper.Map<CatalogItem, CatalogItemDto>(catalogItem)
      };

      return await Task.Run(() => response);
    }
  }
}
