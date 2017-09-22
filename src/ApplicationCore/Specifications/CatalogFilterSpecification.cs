using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : ISpecification<CatalogItem>
    {
        public CatalogFilterSpecification(int? brandId, int? typeId)
        {
            BrandId = brandId;
            TypeId = typeId;
        }

        public int? BrandId { get; }
        public int? TypeId { get; }

        public Expression<Func<CatalogItem, bool>> Criteria => 
            i => (!BrandId.HasValue || i.CatalogBrandId == BrandId) && 
                (!TypeId.HasValue || i.CatalogTypeId == TypeId);

        public List<Expression<Func<CatalogItem, object>>> Includes { get; }  = new List<Expression<Func<CatalogItem, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        public void AddInclude(Expression<Func<CatalogItem, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
