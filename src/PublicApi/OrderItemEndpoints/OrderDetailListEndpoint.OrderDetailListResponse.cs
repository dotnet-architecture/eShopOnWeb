using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderDetailListResponse:BaseResponse
{
    public OrderDetailListResponse(Guid correlationId) : base(correlationId)
    {
    }

    public OrderDetailListResponse()
    {
    }
    public List<OrderDetailDto> OrderDetails { get; internal set; }
}
