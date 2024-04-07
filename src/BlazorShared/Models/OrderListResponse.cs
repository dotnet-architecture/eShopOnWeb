using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorShared.Interfaces;

namespace BlazorShared.Models;

public class OrderListResponse : ILookupDataResponse<OrderList>
{
    [JsonPropertyName("Orders")]
    public List<OrderList> List { get; set; } = new List<OrderList>();
}
