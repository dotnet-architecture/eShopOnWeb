namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class BasketItem : BaseEntity
    {
        //public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public CatalogItem Item { get; set; }
    }
}
