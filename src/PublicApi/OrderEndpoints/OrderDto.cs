using System.Collections.Generic;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public string OrderDate { get; set; }
    public decimal Total { get; set; }
    public string OrderStatus { get; set; }
}
