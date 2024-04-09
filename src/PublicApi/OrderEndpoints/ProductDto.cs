namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
