using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderDetailEndpoints;

public class GetOrderDetailByOrderIdResponse : BaseResponse
{
    public GetOrderDetailByOrderIdResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetOrderDetailByOrderIdResponse()
    {
    }

    public List<OrderItemDto> OrderItems { get; set; }
}
