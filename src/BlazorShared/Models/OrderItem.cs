namespace BlazorShared.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
}
