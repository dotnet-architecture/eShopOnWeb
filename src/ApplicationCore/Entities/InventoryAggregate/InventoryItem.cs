using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.InventoryAggregate
{
    public class InventoryItem : BaseEntity, IAggregateRoot
    {

        public int Quantity { get; set; }

        public int CatalogItemId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset ModifiedDate { get;  set; }

        public InventoryItem()
        {            
        }

        public InventoryItem(int catalogItemId, int quantity, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            Guard.Against.NegativeOrZero(catalogItemId, nameof(catalogItemId));
            Guard.Against.Negative(quantity, nameof(quantity));
            
            CatalogItemId = catalogItemId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;

            SetQuantity(quantity);
        }

        public void UpdateQuantity(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

            Quantity += quantity;
        }

        public void UpdateModifiedDate(DateTimeOffset modifiedDate)
        {
            ModifiedDate = modifiedDate;
        }

        public void SetQuantity(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

            Quantity = quantity;
        }
    }
}
