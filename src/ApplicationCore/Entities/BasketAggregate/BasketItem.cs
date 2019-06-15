namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public int BasketId { get; private set; }
    }
}
