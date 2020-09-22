using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.InventoryAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IAsyncRepository<InventoryItem> _inventoryItemRepository;
        private readonly IAppLogger<InventoryService> _logger;

        public InventoryService(IAsyncRepository<InventoryItem> inventoryItemRepository, IAppLogger<InventoryService> logger)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _logger = logger;
        }

        public async Task<InventoryItem> AddInventoryItem(int catalogItemId, int quantities)
        {
            Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));
            Guard.Against.Negative(quantities, nameof(quantities));

            InventoryItem inventoryItem = new InventoryItem()
            {
                CatalogItemId = catalogItemId,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                Quantity = quantities
            };

            return await _inventoryItemRepository.AddAsync(inventoryItem);
        }

        public async Task<InventoryItem> GetInventoryItemAsync(int catalogItemId)
        {
            Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));

            var inventoryItem = await _inventoryItemRepository.GetByIdAsync(catalogItemId);

            return inventoryItem;
        }

        public async Task<InventoryItem> UpdateInventoryItemQuantityAsync(int catalogItemId, int quantity)
        {
            Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));
            Guard.Against.Negative(quantity, nameof(quantity));

            var existingInventoryItem = await _inventoryItemRepository.GetByIdAsync(catalogItemId);
            existingInventoryItem.UpdateQuantity(quantity);
            existingInventoryItem.UpdateModifiedDate(DateTimeOffset.Now);

            await _inventoryItemRepository.UpdateAsync(existingInventoryItem);

            return existingInventoryItem;
        }

        /// <summary>
        ///  This method is only used to reset back the API to original value.
        /// </summary>
        /// <returns></returns>
        public async Task<string> UpdateAllInventoryItem()
        {
            var allExistingInventoryItems = await _inventoryItemRepository.ListAllAsync();

            foreach (var inventoryItem in allExistingInventoryItems)
            {
                if (inventoryItem.CatalogItemId == 1)
                {
                    inventoryItem.UpdateQuantity(0);
                }
                else if (inventoryItem.CatalogItemId == 2)
                {
                    inventoryItem.UpdateQuantity(1);
                }
                else if (inventoryItem.CatalogItemId == 3)
                {
                    inventoryItem.UpdateQuantity(2);
                }
                else
                {
                    inventoryItem.UpdateQuantity(5);
                }

                inventoryItem.UpdateCreatedDate(DateTimeOffset.Now);
                inventoryItem.UpdateModifiedDate(DateTimeOffset.Now);

                await _inventoryItemRepository.UpdateAsync(inventoryItem);
            }

            return "Done";
        }


    }
}
