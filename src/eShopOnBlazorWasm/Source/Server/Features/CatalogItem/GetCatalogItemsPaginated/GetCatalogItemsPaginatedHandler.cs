namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using AutoMapper;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using System.Threading;
  using System.Threading.Tasks;
  using Microsoft.eShopWeb.ApplicationCore.Specifications;

  public class GetCatalogItemsPaginatedHandler : 
    IRequestHandler<GetCatalogItemsPaginatedRequest, GetCatalogItemsPaginatedResponse>
  {

    public GetCatalogItemsPaginatedHandler(IAsyncRepository<CatalogItem> aCatalogItemRepository, IMapper aMapper)
    {
      CatalogItemRepository = aCatalogItemRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogItem> CatalogItemRepository { get; }
    public IMapper Mapper { get; }

    public async Task<GetCatalogItemsPaginatedResponse> Handle
    (
      GetCatalogItemsPaginatedRequest aGetCatalogItemsPaginatedRequest,
      CancellationToken aCancellationToken
    )
    {
      var catalogFilterPaginatedSpecification =
        new CatalogFilterPaginatedSpecification
        (
          skip: aGetCatalogItemsPaginatedRequest.PageSize * aGetCatalogItemsPaginatedRequest.PageIndex, 
          take: aGetCatalogItemsPaginatedRequest.PageSize,
          brandId: aGetCatalogItemsPaginatedRequest.CatalogBrandId,
          typeId: aGetCatalogItemsPaginatedRequest.CatalogTypeId
        );
      IReadOnlyList<CatalogItem> catalogItems = await CatalogItemRepository.ListAsync(catalogFilterPaginatedSpecification);
      var response = new GetCatalogItemsPaginatedResponse(aGetCatalogItemsPaginatedRequest.RequestId);

      response.CatalogItems.AddRange(catalogItems.Select(Mapper.Map<CatalogItemDto>));

      return await Task.Run(() => response);
    }
  }
}
