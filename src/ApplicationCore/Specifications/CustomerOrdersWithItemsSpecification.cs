using ApplicationCore.Interfaces;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Specifications
{
    public class CustomerOrdersWithItemsSpecification : ISpecification<Order>
    {
        private readonly string _buyerId;

        public CustomerOrdersWithItemsSpecification(string buyerId)
        {
            _buyerId = buyerId;
            AddInclude(o => o.OrderItems);
        }

        public Expression<Func<Order, bool>> Criteria => o => o.BuyerId == _buyerId;

        public List<Expression<Func<Order, object>>> Includes { get; } = new List<Expression<Func<Order, object>>>();

        public void AddInclude(Expression<Func<Order, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
