namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using AutoMapper;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using System.Threading;
  using System.Threading.Tasks;

  public class CreateCatalogItemHandler : IRequestHandler<CreateCatalogItemRequest, CreateCatalogItemResponse>
  {

    public CreateCatalogItemHandler(IAsyncRepository<CatalogItem> aCatalogItemRepository, IMapper aMapper)
    {
      CatalogItemRepository = aCatalogItemRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogItem> CatalogItemRepository { get; }
    public IMapper Mapper { get; }

    public async Task<CreateCatalogItemResponse> Handle
    (
      CreateCatalogItemRequest aCreateCatalogItemRequest,
      CancellationToken aCancellationToken
    )
    {
      var response = new CreateCatalogItemResponse(aCreateCatalogItemRequest.RequestId);
      CatalogItem catalogItem = Mapper.Map<CreateCatalogItemRequest, CatalogItem>(aCreateCatalogItemRequest);
      catalogItem = await CatalogItemRepository.AddAsync(catalogItem);
      response.CatalogItem = Mapper.Map<CatalogItem, CatalogItemDto>(catalogItem);

      return await Task.Run(() => response);
    }
  }
}
