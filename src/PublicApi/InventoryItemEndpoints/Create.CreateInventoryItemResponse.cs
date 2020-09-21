using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.InventoryItemEndpoints
{
    public class CreateInventoryItemResponse : BaseResponse
    {
        public CreateInventoryItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateInventoryItemResponse()
        {
        }

        public InventoryItemDto InventoryItem { get; set; }
    }
}
