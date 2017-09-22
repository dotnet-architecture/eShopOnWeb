using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpecification : ISpecification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            BasketId = basketId;
            AddInclude(b => b.Items);
        }
        public BasketWithItemsSpecification(string buyerId)
        {
            BuyerId = buyerId;
            AddInclude(b => b.Items);
        }

        public int? BasketId { get; }
        public string BuyerId { get; }

        public Expression<Func<Basket, bool>> Criteria => b =>
            (BasketId.HasValue && b.Id == BasketId.Value)
            || (BuyerId != null && b.BuyerId == BuyerId);

        public List<Expression<Func<Basket, object>>> Includes { get; } = new List<Expression<Func<Basket, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        public void AddInclude(Expression<Func<Basket, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
