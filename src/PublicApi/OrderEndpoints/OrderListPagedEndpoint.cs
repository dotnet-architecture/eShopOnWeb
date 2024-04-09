using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorShared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderListEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderListPagedEndpoint;

/// <summary>
/// List Orders (paged)
/// </summary>
public class OrderListPagedEndpoint : IEndpoint<IResult, ListOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderListPagedEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
            async (int? pageSize, int? pageIndex, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new ListOrderRequest(pageSize, pageIndex), itemRepository);
            })
            .Produces<ListOrderResponse>()
            .WithTags("ListOrderEndpoints");
    }

    public async Task<IResult> HandleAsync(ListOrderRequest request, IRepository<Order> itemRepository)
    {
        await Task.Delay(1000);
        var response = new ListOrderResponse(request.CorrelationId());

        int totalItems = await itemRepository.CountAsync();

        var pagedSpec = new ListOrderPaginatedSpecification(
            skip: request.PageIndex * request.PageSize,
            take: request.PageSize);

        pagedSpec = pagedSpec.IncludeOrderItems();

        var items = await itemRepository.ListAsync(pagedSpec);

        response.Orders.AddRange(items.Select(_mapper.Map<OrderDto>));

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
