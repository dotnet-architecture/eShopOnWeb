using System.Security.Policy;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public string PictureUri { get; set; }
    public string OrderStatus { get; set; }
    public string OrderDate { get; set; }
}
