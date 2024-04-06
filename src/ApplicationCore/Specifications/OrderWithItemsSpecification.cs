using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderWithItemsSpecification:Specification<Order>
{
    public OrderWithItemsSpecification()
    {
        Query.Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered)
            .ThenInclude(c => c.CatalogItemId);
            
    }
}
