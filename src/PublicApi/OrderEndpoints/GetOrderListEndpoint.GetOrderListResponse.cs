using System;
using System.Collections.Generic;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class GetOrderListResponse : BaseResponse
{
    public GetOrderListResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetOrderListResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}
