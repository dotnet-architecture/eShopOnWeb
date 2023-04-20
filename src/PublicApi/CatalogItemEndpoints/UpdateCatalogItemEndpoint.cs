using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

/// <summary>
/// Updates a Catalog Item
/// </summary>
public class UpdateCatalogItemEndpoint : IEndpoint<IResult, UpdateCatalogItemRequest, IRepository<CatalogItem>>
{ 
    private readonly IUriComposer _uriComposer;

    public UpdateCatalogItemEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/catalog-items",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (UpdateCatalogItemRequest request, IRepository<CatalogItem> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<UpdateCatalogItemResponse>()
            .WithTags("CatalogItemEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
    {
        var response = new UpdateCatalogItemResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.Id);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        CatalogItem.CatalogItemDetails details = new(request.Name, request.Description, request.Price);
        existingItem.UpdateDetails(details);
        existingItem.UpdateBrand(request.CatalogBrandId);
        existingItem.UpdateType(request.CatalogTypeId);

        await itemRepository.UpdateAsync(existingItem);

        var dto = new CatalogItemDto
        {
            Id = existingItem.Id,
            CatalogBrandId = existingItem.CatalogBrandId,
            CatalogTypeId = existingItem.CatalogTypeId,
            Description = existingItem.Description,
            Name = existingItem.Name,
            PictureUri = _uriComposer.ComposePicUri(existingItem.PictureUri),
            Price = existingItem.Price
        };
        response.CatalogItem = dto;
        return Results.Ok(response);
    }
}
