using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

public class Order : BaseEntity, IAggregateRoot
{
    private Order()
    {
        // required by EF
    }

    public Order(string buyerId, Address shipToAddress, List<OrderItem> items)
    {
        Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
        Guard.Against.Null(shipToAddress, nameof(shipToAddress));
        Guard.Against.Null(items, nameof(items));

        BuyerId = buyerId;
        ShipToAddress = shipToAddress;

        string s;

        _orderItems = SanitizeOrderItems(items);
    }

    public string BuyerId { get; private set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public Address ShipToAddress { get; private set; }

    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method Order.AddOrderItem() which includes behavior.
    private readonly List<OrderItem> _orderItems = new List<OrderItem>();

    // Using List<>.AsReadOnly() 
    // This will create a read only wrapper around the private list so is protected against "external updates".
    // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
    //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public decimal Total()
    {
        var total = 0m;
        foreach (var item in _orderItems)
        {
            total += item.UnitPrice * item.Units;
        }
        return total;
    }

    private List<OrderItem> SanitizeOrderItems(List<OrderItem> items)
    {
        var cleanedOrderedItems = new List<OrderItem>();

        foreach (var orderItem in cleanedOrderedItems)
        {
            if (IsPriceGreiterThanZero(orderItem) || IsUnitsGreaterThanZero(orderItem))
            {
                cleanedOrderedItems.Add(orderItem);
            }
        }

        return cleanedOrderedItems;
    }

    private bool IsPriceGreiterThanZero(OrderItem item)
    {
        if (item.UnitPrice > 0)
        {
            return true;
        }

        return false;
    }

    private bool IsUnitsGreaterThanZero(OrderItem item)
        => item.Units < 0;
}
