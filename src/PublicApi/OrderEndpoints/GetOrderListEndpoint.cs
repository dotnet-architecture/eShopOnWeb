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

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class GetOrderListEndpoint : IEndpoint<IResult, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public GetOrderListEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
                async (IRepository<Order> orderRepository) =>
                {
                    return await HandleAsync(orderRepository);
                })
            .Produces<GetOrderListResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Order> request)
    {
        var spec = new OrderWithItemsSpecification();
        var orders = await request.ListAsync(spec);

        if (orders is null)
            return Results.NotFound();

        //calculate total for each order
        foreach (var order in orders)
        {
            order.Total();
        }

        var response = new GetOrderListResponse();
        response.Orders.AddRange(orders.Select(_mapper.Map<OrderDto>));
        return Results.Ok(response.Orders);
    }

    //public async Task<IResult> HandleAsync(IRepository<Order> orderRepository)
    //{
    //    var response = new GetOrderListResponse();

    //    var orders = await orderRepository.ListAsync();

    //    if (orders is null)
    //        return Results.NotFound();


    //    response.Orders.AddRange(orders.Select(_mapper.Map<OrderDto>));
    //    return Results.Ok(response.Orders);
    //}
}
