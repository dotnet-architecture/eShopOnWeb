using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.InventoryItemEndpoints
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        
        public int CatalogItemId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset CreatedDate { get; private set; }

        public DateTimeOffset ModifiedDate { get; set; }

    }
}
