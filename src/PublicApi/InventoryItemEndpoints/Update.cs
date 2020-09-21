using Ardalis.ApiEndpoints;
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
    public class Update : BaseAsyncEndpoint<UpdateInventoryItemRequest, UpdateInventoryItemResponse>
    {
        private readonly IInventoryService _inventoryService;

        public Update(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPut("api/inventory-items")]
        [SwaggerOperation(
            Summary = "Update Inventory details by Catalog Item Id",
            Description = "Update a Inventory by Catalog Item Id",
            OperationId = "inventory-items.Update",
            Tags = new[] { "InventoryItemEndpoints" })
        ]
        public override async Task<ActionResult<UpdateInventoryItemResponse>> HandleAsync(UpdateInventoryItemRequest request, CancellationToken cancellationToken)
        {

            var response = new UpdateInventoryItemResponse(request.CorrelationId());

            var existingItem = await _inventoryService.UpdateInventoryItemQuantityAsync(request.CatalogItemId, request.Quantity);

            response.InventoryItem = new InventoryItemDto
            {
                Id = existingItem.Id,
                CatalogItemId = existingItem.CatalogItemId,
                Quantity = existingItem.Quantity,
                ModifiedDate = existingItem.ModifiedDate
            };

            return Ok(response);

        }
    }
}
