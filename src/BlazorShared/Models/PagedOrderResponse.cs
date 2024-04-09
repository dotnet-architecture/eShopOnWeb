using System.Collections.Generic;

namespace BlazorShared.Models;

public class PagedOrderResponse
{
    public List<OrderModel> orders { get; set; } = new List<OrderModel>();
    public int PageCount { get; set; } = 0;
}

