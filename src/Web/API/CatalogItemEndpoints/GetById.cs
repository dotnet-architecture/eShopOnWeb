using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class GetById : BaseAsyncEndpoint<GetByIdCatalogItemRequest, GetByIdCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public GetById(IAsyncRepository<CatalogItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("api/catalog-items/{CatalogItemId}")]
        [SwaggerOperation(
            Summary = "Get a Catalog Item by Id",
            Description = "Gets a Catalog Item by Id",
            OperationId = "catalog-items.GetById",
            Tags = new[] { "CatalogItemEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCatalogItemResponse>> HandleAsync([FromRoute]GetByIdCatalogItemRequest request)
        {
            var response = new GetByIdCatalogItemResponse(request.CorrelationId());

            var item = await _itemRepository.GetByIdAsync(request.CatalogItemId);
            if (item is null) return NotFound();

            response.CatalogItem = new CatalogItemDto
            {
                Id = item.Id,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                Description = item.Description,
                Name = item.Name,
                PictureUri = item.PictureUri,
                Price = item.Price
            };
            return Ok(response);
        }
    }
}
