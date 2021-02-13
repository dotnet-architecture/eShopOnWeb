using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints
{
    public class List : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<ListCatalogBrandsResponse>
    {
        private readonly IAsyncRepository<CatalogBrand> _catalogBrandRepository;
        private readonly IMapper _mapper;

        public List(IAsyncRepository<CatalogBrand> catalogBrandRepository,
            IMapper mapper)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        [HttpGet("api/catalog-brands")]
        [SwaggerOperation(
            Summary = "List Catalog Brands",
            Description = "List Catalog Brands",
            OperationId = "catalog-brands.List",
            Tags = new[] { "CatalogBrandEndpoints" })
        ]
        public override async Task<ActionResult<ListCatalogBrandsResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var response = new ListCatalogBrandsResponse();

            var items = await _catalogBrandRepository.ListAllAsync(cancellationToken);

            response.CatalogBrands.AddRange(items.Select(_mapper.Map<CatalogBrandDto>));

            return Ok(response);
        }
    }
}
