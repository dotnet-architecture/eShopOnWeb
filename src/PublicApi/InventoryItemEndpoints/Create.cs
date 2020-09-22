using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.InventoryItemEndpoints
{
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint<CreateInventoryItemRequest, CreateInventoryItemResponse>
    {
        private readonly IInventoryService _inventoryService;

        public Create(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("api/inventory-items")]
        [SwaggerOperation(
            Summary = "Add an Inventory Item",
            Description = "Add an Inventory Item",
            OperationId = "inventory-items.Create",
            Tags = new[] { "InventoryItemEndpoints" })
        ]     
        public override async Task<ActionResult<CreateInventoryItemResponse>> HandleAsync(CreateInventoryItemRequest request, CancellationToken cancellationToken = default)
        {
            var response = new CreateInventoryItemResponse(request.CorrelationId());

            var newItem = await _inventoryService.AddInventoryItem(request.CatalogItemId, request.Quantity);

            response.InventoryItem = new InventoryItemDto
            {
                Id = newItem.Id,
                CatalogItemId = newItem.CatalogItemId,
                Quantity = newItem.Quantity,                
                ModifiedDate = newItem.ModifiedDate
            };

            return Ok(response);
        }
    }
}
