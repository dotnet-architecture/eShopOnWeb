using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderDetailEndpoints;

public class GetOrderDetailByOrderIdEndpoint : IEndpoint<IResult, GetOrderDetailByOrderIdRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public GetOrderDetailByOrderIdEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-details/{orderId}",
            async (int orderId, IRepository<Order> orderRepository) =>
            {
                return await HandleAsync(new GetOrderDetailByOrderIdRequest(orderId), orderRepository);
            })
            .Produces<GetOrderDetailByOrderIdResponse>()
            .WithTags("OrderDetailEndpoints");
    }

    public async Task<IResult> HandleAsync(GetOrderDetailByOrderIdRequest request, IRepository<Order> orderRepository)
    {
        var spec = new OrderWithItemsByIdSpec(request.OrderId);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null)
            return Results.NotFound();

        var orderItems = order.OrderItems.ToList();
        var orderItemsDto = _mapper.Map<List<OrderItemDto>>(orderItems);

        var response = new GetOrderDetailByOrderIdResponse(request.CorrelationId());
        response.OrderItems = order.OrderItems.Select(item => new OrderItemDto
        {
            ProductName = item.ItemOrdered.ProductName,
            UnitPrice = item.UnitPrice,
            Units = item.Units
        }).ToList();

        return Results.Ok(response.OrderItems);

    }
}
