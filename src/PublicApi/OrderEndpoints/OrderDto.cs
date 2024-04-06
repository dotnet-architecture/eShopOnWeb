namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public string OrderDate { get; set; }
    public decimal Total { get; set; }
    public string OrderStatus { get; set; }
}
