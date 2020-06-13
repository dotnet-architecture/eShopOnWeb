namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using AutoMapper;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System.Threading;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.CatalogItem;

  public class GetCatalogItemHandler : IRequestHandler<GetCatalogItemRequest, GetCatalogItemResponse>
  {
    public GetCatalogItemHandler(IAsyncRepository<CatalogItem> aCatalogItemRepository, IMapper aMapper)
    {
      CatalogItemRepository = aCatalogItemRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogItem> CatalogItemRepository { get; }
    public IMapper Mapper { get; }

    public async Task<GetCatalogItemResponse> Handle
    (
      GetCatalogItemRequest aGetCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      CatalogItem catalogItem = await CatalogItemRepository.GetByIdAsync(aGetCatalogItemRequest.CatalogItemId);
      var response = new GetCatalogItemResponse(aGetCatalogItemRequest.RequestId)
      {
        CatalogItem = Mapper.Map<CatalogItem, CatalogItemDto>(catalogItem)
      };

      return response;
    }
  }
}
