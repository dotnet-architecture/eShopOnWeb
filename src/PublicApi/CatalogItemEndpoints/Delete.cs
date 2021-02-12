using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteCatalogItemRequest>
        .WithResponse<DeleteCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public Delete(IAsyncRepository<CatalogItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpDelete("api/catalog-items/{CatalogItemId}")]
        [SwaggerOperation(
            Summary = "Deletes a Catalog Item",
            Description = "Deletes a Catalog Item",
            OperationId = "catalog-items.Delete",
            Tags = new[] { "CatalogItemEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCatalogItemResponse>> HandleAsync([FromRoute] DeleteCatalogItemRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCatalogItemResponse(request.CorrelationId());

            var itemToDelete = await _itemRepository.GetByIdAsync(request.CatalogItemId, cancellationToken);
            if (itemToDelete is null) return NotFound();

            await _itemRepository.DeleteAsync(itemToDelete, cancellationToken);

            return Ok(response);
        }
    }
}
