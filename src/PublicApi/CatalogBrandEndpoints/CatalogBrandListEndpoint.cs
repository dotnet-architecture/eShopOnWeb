using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;

/// <summary>
/// List Catalog Brands
/// </summary>
public class CatalogBrandListEndpoint : IEndpoint<IResult>
{
    private IRepository<CatalogBrand> _catalogBrandRepository;
    private readonly IMapper _mapper;

    public CatalogBrandListEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-brands",
            async (IRepository<CatalogBrand> catalogBrandRepository) =>
            {
                _catalogBrandRepository = catalogBrandRepository;
                return await HandleAsync();
            })
           .Produces<ListCatalogBrandsResponse>()
           .WithTags("CatalogBrandEndpoints");
    }

    public async Task<IResult> HandleAsync()
    {
        var response = new ListCatalogBrandsResponse();

        var items = await _catalogBrandRepository.ListAsync();

        response.CatalogBrands.AddRange(items.Select(_mapper.Map<CatalogBrandDto>));

        return Results.Ok(response);
    }
}
