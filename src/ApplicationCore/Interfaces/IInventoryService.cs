using Microsoft.eShopWeb.ApplicationCore.Entities.InventoryAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryItem> UpdateInventoryItemQuantityAsync(int catalogItemId, int quantities);

        Task<InventoryItem> AddInventoryItem(int catalogItemId, int quantities);

        Task<InventoryItem> GetInventoryItemAsync(int catalogItemId);

        Task<string> UpdateAllInventoryItem();
    }
}
