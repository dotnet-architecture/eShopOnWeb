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
    public class GetById : BaseAsyncEndpoint<GetByIdInventoryItemRequest, GetByIdInventoryItemResponse>
    {
        private readonly IInventoryService _inventoryService;        

        public GetById(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;            
        }

        [HttpGet("api/inventory-items/{CatalogItemId}")]
        [SwaggerOperation(
            Summary = "Get Inventory details by Catalog Item Id",
            Description = "Gets a Inventory by Catalog Item Id",
            OperationId = "inventory-items.GetById",
            Tags = new[] { "InventoryItemEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdInventoryItemResponse>> HandleAsync([FromRoute] GetByIdInventoryItemRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdInventoryItemResponse(request.CorrelationId());

            var item = await _inventoryService.GetInventoryItemAsync(request.CatalogItemId);
            if (item is null) return NotFound();

            response.InventoryItem = new InventoryItemDto
            {
                Id = item.Id,
                CatalogItemId = item.CatalogItemId,
                Quantity = item.Quantity,
                ModifiedDate = item.ModifiedDate
            };

            return Ok(response);
        }
    }
}
