using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
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

            try
            {
                existingItem.UpdateDetails(request.Name, request.Description, request.Price);
            }
            catch (AggregateException e)
                when ((e.InnerException) is DuplicateCatalogItemNameException dupeException)
            {
                this.ModelState.AddModelError("name", $"Duplicate name not permitted. Name is duplicate of item id {dupeException.DuplicateItemId}.");
                return BadRequest(ModelState);

                // TODO: Look into ProblemDetails https://tools.ietf.org/html/rfc7807
                // nuget Hellang.ProblemDetails
                // https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-3.1#validation-failure-error-response
                // return base.Problem()
            }

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
