using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogItemsSpecification : Specification<CatalogItem>
    {
        public CatalogItemsSpecification(params int[] ids)
        {
            Query.Where(c => ids.Contains(c.Id));
        }
    }
}
