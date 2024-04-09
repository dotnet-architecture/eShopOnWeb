using System.Collections.Generic;
using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public List<ProductDto> OrderedProducts { get; set; }
}
