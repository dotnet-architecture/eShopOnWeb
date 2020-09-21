using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.InventoryItemEndpoints
{
    public class UpdateInventoryItemResponse : BaseResponse
    {
        public UpdateInventoryItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateInventoryItemResponse()
        {
        }

        public InventoryItemDto InventoryItem { get; set; }
    }
}
