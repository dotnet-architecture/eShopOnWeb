using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class ListOrderPaginatedSpecification : Specification<Order>
{
    public ListOrderPaginatedSpecification(int skip, int take)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Skip(skip).Take(take);
    }

    public ListOrderPaginatedSpecification IncludeOrderItems()
    {
        Query.Include(o => o.OrderItems);
        return this;
    }
}

