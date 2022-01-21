using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;

/// <summary>
/// List Catalog Types
/// </summary>
public class CatalogTypeListEndpoint : IEndpoint<IResult>
{
    private IRepository<CatalogType> _catalogTypeRepository;
    private readonly IMapper _mapper;

    public CatalogTypeListEndpoint(IMapper mapper)
    {      
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-types", 
            async (IRepository<CatalogType> catalogTypeRepository) =>
            {
                _catalogTypeRepository = catalogTypeRepository;
                return await HandleAsync();
            })
            .Produces<ListCatalogTypesResponse>()
            .WithTags("CatalogTypeEndpoints");
    }

    public async Task<IResult> HandleAsync()
    {
        var response = new ListCatalogTypesResponse();

        var items = await _catalogTypeRepository.ListAsync();

        response.CatalogTypes.AddRange(items.Select(_mapper.Map<CatalogTypeDto>));

        return Results.Ok(response);
    }
}
