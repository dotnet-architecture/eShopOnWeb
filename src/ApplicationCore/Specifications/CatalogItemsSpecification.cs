using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogItemsSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogItemsSpecification(params int[] ids) : base(c => ids.Contains(c.Id))
        {

        }
    }
}
