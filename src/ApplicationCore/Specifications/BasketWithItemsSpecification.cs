using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpecification : ISpecification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            BasketId = basketId;
            AddInclude(b => b.Items);
        }

        public int BasketId { get; }

        public Expression<Func<Basket, bool>> Criteria => b => b.Id == BasketId;

        public List<Expression<Func<Basket, object>>> Includes { get; } = new List<Expression<Func<Basket, object>>>();

        public void AddInclude(Expression<Func<Basket, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
