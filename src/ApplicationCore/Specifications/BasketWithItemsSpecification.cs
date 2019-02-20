using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class BasketWithItemsSpecification : BaseSpecification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
            :base(b => b.Id == basketId)
        {
            AddInclude(b => b.Items);
        }
        public BasketWithItemsSpecification(string buyerId)
            :base(b => b.BuyerId == buyerId)
        {
            AddInclude(b => b.Items);
        }
    }
}
