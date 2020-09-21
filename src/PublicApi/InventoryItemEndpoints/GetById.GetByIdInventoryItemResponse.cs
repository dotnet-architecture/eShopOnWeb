using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.InventoryItemEndpoints
{
    public class GetByIdInventoryItemResponse : BaseResponse
    {
        public GetByIdInventoryItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdInventoryItemResponse()
        {
        }

        public InventoryItemDto InventoryItem { get; set; }

    }
}
