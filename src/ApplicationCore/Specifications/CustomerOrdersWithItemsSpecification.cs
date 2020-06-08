using Ardalis.Specification;
using Ardalis.Specification.QueryExtensions.Include;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CustomerOrdersWithItemsSpecification : BaseSpecification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string buyerId)
            : base(o => o.BuyerId == buyerId)
        {
            AddIncludes(query => query.Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered));
        }
    }
}
