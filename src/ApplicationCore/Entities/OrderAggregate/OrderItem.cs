namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }

        public int Units { get; private set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }

        /// <summary>
        /// Discount price by specific factor.
        /// </summary>
        /// <param name="discount">Value from 0 to 1 or finer</param>
        public void ApplyDiscount(double discount)
        {
            this.UnitPrice *= (decimal) discount;
        }
    }
}