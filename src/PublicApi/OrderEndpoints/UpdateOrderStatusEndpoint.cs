using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.OrderStatusEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;


public class UpdateOrderStatusEndpoint : IEndpoint<IResult, UpdateOrderStatusRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;

    public UpdateOrderStatusEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (UpdateOrderStatusRequest request, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<UpdateOrderStatusResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderStatusRequest request, IRepository<Order> itemRepository)
    {
        var response = new UpdateOrderStatusResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.Id);

        if (existingItem == null)
        {
            return Results.NotFound();
        }

        existingItem.Status = request.Status;

        await itemRepository.UpdateAsync(existingItem);

        response.Message = "Successfull";
        return Results.Ok(response);
    }
}
