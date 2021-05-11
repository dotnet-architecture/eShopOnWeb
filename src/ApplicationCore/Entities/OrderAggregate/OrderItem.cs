namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }

        public int Discount { get; private set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units, int discountValue = 0)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
            Discount = discountValue;
        }
    }
}
