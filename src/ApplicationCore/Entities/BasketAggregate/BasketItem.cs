using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int CatalogItemId { get; private set; }
        public int BasketId { get; private set; }

        public BasketItem(int catalogItemId, int quantity, decimal unitPrice)
        {
            CatalogItemId = catalogItemId;
            UnitPrice = unitPrice;
            SetQuantity(quantity);
        }

        public void AddQuantity(int quantity)
        {
            SetQuantity(Quantity + quantity);
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException($"{nameof(Quantity)} can't be less than 0.");
            
            Quantity = quantity;
        }
    }
}
