using System;

namespace Microsoft.eShopWeb.PublicApi.OrderStatusEndpoints;

public class UpdateOrderStatusResponse : BaseResponse
{
    public UpdateOrderStatusResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateOrderStatusResponse()
    {
    }

    public string Message { get; set; }
}
