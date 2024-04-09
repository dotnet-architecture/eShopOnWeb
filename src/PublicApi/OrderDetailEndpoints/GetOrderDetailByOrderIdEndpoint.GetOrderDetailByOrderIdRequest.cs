namespace Microsoft.eShopWeb.PublicApi.OrderDetailEndpoints;

public class GetOrderDetailByOrderIdRequest : BaseRequest
{
    public int OrderId { get; set; }

    public GetOrderDetailByOrderIdRequest(int orderId)
    {
        OrderId = orderId;
    }
}
