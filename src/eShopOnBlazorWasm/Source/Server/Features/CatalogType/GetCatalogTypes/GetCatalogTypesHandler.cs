namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  using AutoMapper;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  public class GetCatalogTypesHandler : IRequestHandler<GetCatalogTypesRequest, GetCatalogTypesResponse>
  {
    public GetCatalogTypesHandler(IAsyncRepository<CatalogType> aCatalogTypeRepository, IMapper aMapper)
    {
      CatalogTypeRepository = aCatalogTypeRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogType> CatalogTypeRepository { get; }
    public IMapper Mapper { get; }

    public async Task<GetCatalogTypesResponse> Handle
    (
      GetCatalogTypesRequest aGetCatalogTypesRequest,
      CancellationToken aCancellationToken
    )
    {
      IReadOnlyList<CatalogType> catalogTypes = await CatalogTypeRepository.ListAllAsync();
      
      var response = new GetCatalogTypesResponse(aGetCatalogTypesRequest.CorrelationId);
      response.CatalogTypes.AddRange(catalogTypes.Select(Mapper.Map<CatalogTypeDto>));

      return response;
    }
  }
}
