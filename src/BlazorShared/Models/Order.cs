using System;
using System.Collections.Generic;
using BlazorShared.Attributes;
using BlazorShared.Enums;

namespace BlazorShared.Models;

[Endpoint(Name = "orders")]
public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
