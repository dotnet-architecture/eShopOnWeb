using System.Collections.Generic;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class Basket : BaseEntity<string>
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
