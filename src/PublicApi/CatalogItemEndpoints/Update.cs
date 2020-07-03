using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Update : BaseAsyncEndpoint<UpdateCatalogItemRequest, UpdateCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public Update(IAsyncRepository<CatalogItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPut("api/catalog-items")]
        [SwaggerOperation(
            Summary = "Updates a Catalog Item",
            Description = "Updates a Catalog Item",
            OperationId = "catalog-items.update",
            Tags = new[] { "CatalogItemEndpoints" })
        ]
        public override async Task<ActionResult<UpdateCatalogItemResponse>> HandleAsync(UpdateCatalogItemRequest request)
        {
            var response = new UpdateCatalogItemResponse(request.CorrelationId());

            var existingItem = await _itemRepository.GetByIdAsync(request.Id);

            existingItem.UpdateDetails(request.Name, request.Description, request.Price);
            existingItem.UpdateBrand(request.CatalogBrandId);
            existingItem.UpdateType(request.CatalogTypeId);

            await _itemRepository.UpdateAsync(existingItem);

            var dto = new CatalogItemDto
            {
                Id = existingItem.Id,
                CatalogBrandId = existingItem.CatalogBrandId,
                CatalogTypeId = existingItem.CatalogTypeId,
                Description = existingItem.Description,
                Name = existingItem.Name,
                PictureUri = existingItem.PictureUri,
                Price = existingItem.Price
            };
            response.CatalogItem = dto;
            return response;
        }
    }
}
