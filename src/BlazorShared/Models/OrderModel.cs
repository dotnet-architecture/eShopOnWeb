using System;
using System.Collections.Generic;

namespace BlazorShared.Models;

public class OrderModel
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public List<ProductModel> OrderedProducts { get; set; }

}
