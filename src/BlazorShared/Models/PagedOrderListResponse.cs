using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorShared.Interfaces;

namespace BlazorShared.Models;

public class PagedOrderListResponse
{
    [JsonPropertyName("Orders")]
    public List<Order> Orders { get; set; } = new List<Order>();
    public int PageCount { get; set; } = 0;
}
