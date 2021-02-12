using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetByIdCatalogItemRequest>
        .WithResponse<GetByIdCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IUriComposer _uriComposer;

        public GetById(IAsyncRepository<CatalogItem> itemRepository, IUriComposer uriComposer)
        {
            _itemRepository = itemRepository;
            _uriComposer = uriComposer;
        }

        [HttpGet("api/catalog-items/{CatalogItemId}")]
        [SwaggerOperation(
            Summary = "Get a Catalog Item by Id",
            Description = "Gets a Catalog Item by Id",
            OperationId = "catalog-items.GetById",
            Tags = new[] { "CatalogItemEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCatalogItemResponse>> HandleAsync([FromRoute] GetByIdCatalogItemRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdCatalogItemResponse(request.CorrelationId());

            var item = await _itemRepository.GetByIdAsync(request.CatalogItemId, cancellationToken);
            if (item is null) return NotFound();

            response.CatalogItem = new CatalogItemDto
            {
                Id = item.Id,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                Description = item.Description,
                Name = item.Name,
                PictureUri = _uriComposer.ComposePicUri(item.PictureUri),
                Price = item.Price
            };
            return Ok(response);
        }
    }
}
