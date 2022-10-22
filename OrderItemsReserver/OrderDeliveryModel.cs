using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace OrderItemsReserver;
public class OrderDeliveryModel : TableEntity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string ShippingAddress { get; set; }
    public Dictionary<string, int> ListOfItems { get; set; }
    public decimal FinalPrice { get; set; }
}
