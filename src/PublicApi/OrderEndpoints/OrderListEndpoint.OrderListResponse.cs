using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListResponse: BaseResponse
{
    public OrderListResponse(Guid correlationId) : base(correlationId)
    {
    }

    public OrderListResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}
