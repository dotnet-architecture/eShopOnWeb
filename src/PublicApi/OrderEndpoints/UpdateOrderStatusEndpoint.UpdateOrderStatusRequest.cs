namespace Microsoft.eShopWeb.PublicApi.OrderStatusEndpoints;

public class UpdateOrderStatusRequest : BaseRequest
{
    public int Id { get; set; }
    public string Status { get; set; }
}
