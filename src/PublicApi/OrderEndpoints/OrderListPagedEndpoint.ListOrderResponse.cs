using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderListEndpoints;

public class ListOrderResponse : BaseResponse
{
    public ListOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListOrderResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public int PageCount { get; set; }
}
