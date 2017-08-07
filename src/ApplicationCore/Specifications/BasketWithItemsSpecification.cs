using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpecification : ISpecification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            BasketId = basketId;
        }

        public int BasketId { get; }

        public Expression<Func<Basket, bool>> Criteria => b => b.Id == BasketId;

        public Expression<Func<Basket, object>> Include => b => b.Items;
    }
}
