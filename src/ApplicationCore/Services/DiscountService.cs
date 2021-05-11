using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly int ThreeItemsDiscount = 10;
        private readonly int FiveItemsDiscount = 15;



        public OrderItem ApplyDiscountOnItem(OrderItem item)
        {
          return new OrderItem(item.ItemOrdered, item.UnitPrice, item.Units, ResolveDiscount(item.Units));
        }

        public Order ApplyDiscountOnOrder
        private int ResolveDiscount(int itemsUnit)
        {
            if(itemsUnit == 3)
            {
                return ThreeItemsDiscount;
            }

            if(itemsUnit == 5)
            {
                return FiveItemsDiscount;
            }

            return 0;
        }


    }

    public class Discount : BaseEntity
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public int DiscountValue { get; set; }
        public RuleType RuleTypeId { get;set; }        
        public int RuleCondition { get; set; }
        public bool IsActive { get; set; }
    }

    public enum RuleType
    {
        Quantity,
        Constant,
        BasketValue
    }
}
