using BlazorShared.Enums;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
