namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using AutoMapper;
  using MediatR;
  using Microsoft.eShopWeb.ApplicationCore.Entities;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  
  public class GetCatalogBrandsHandler : IRequestHandler<GetCatalogBrandsRequest, GetCatalogBrandsResponse>
  {
    public GetCatalogBrandsHandler(IAsyncRepository<CatalogBrand> aCatalogBrandRepository, IMapper aMapper)
    {
      CatalogBrandRepository = aCatalogBrandRepository;
      Mapper = aMapper;
    }

    public IAsyncRepository<CatalogBrand> CatalogBrandRepository { get; }
    public IMapper Mapper { get; }

    public async Task<GetCatalogBrandsResponse> Handle
    (
      GetCatalogBrandsRequest aGetCatalogBrandsRequest,
      CancellationToken aCancellationToken
    )
    {
      IReadOnlyList<CatalogBrand> catalogBrands = await CatalogBrandRepository.ListAllAsync();
      var response = new GetCatalogBrandsResponse(aGetCatalogBrandsRequest.Id);
      response.CatalogBrands.AddRange(catalogBrands.Select(Mapper.Map<CatalogBrandDto>));

      return await Task.Run(() => response);
    }
  }
}
