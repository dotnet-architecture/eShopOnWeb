using System.Threading.Tasks;
using BlazorShared.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;


namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        // Update an Order by Id and int status
        app.MapPut("api/order-update",
            async (UpdateOrderRequest request, IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(request, orderRepository);
                })
            .Produces<UpdateOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> orderRepository)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());
        var existingOrder = await orderRepository.GetByIdAsync(request.Id);
        if (existingOrder == null)
            return Results.NotFound();

        existingOrder.OrderStatus = (OrderStatus)request.OrderStatus;
        await orderRepository.UpdateAsync(existingOrder);

        var dto = new OrderDto
        {
            Id = existingOrder.Id,
            OrderStatus = existingOrder.OrderStatus,
            OrderDate = existingOrder.OrderDate,
            Total = existingOrder.Total()
        };
        response.Order = dto;
        return Results.Ok(response.Order);
    }
}
