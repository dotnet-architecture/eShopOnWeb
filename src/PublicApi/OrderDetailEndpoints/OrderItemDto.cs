namespace Microsoft.eShopWeb.PublicApi.OrderDetailEndpoints;

public class OrderItemDto
{
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
