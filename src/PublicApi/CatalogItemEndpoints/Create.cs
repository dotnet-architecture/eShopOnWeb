using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{

    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateCatalogItemRequest>
        .WithResponse<CreateCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IUriComposer _uriComposer;

        public Create(IAsyncRepository<CatalogItem> itemRepository, IUriComposer uriComposer)
        {
            _itemRepository = itemRepository;
            _uriComposer = uriComposer;
        }

        [HttpPost("api/catalog-items")]
        [SwaggerOperation(
            Summary = "Creates a new Catalog Item",
            Description = "Creates a new Catalog Item",
            OperationId = "catalog-items.create",
            Tags = new[] { "CatalogItemEndpoints" })
        ]
        public override async Task<ActionResult<CreateCatalogItemResponse>> HandleAsync(CreateCatalogItemRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateCatalogItemResponse(request.CorrelationId());

            var newItem = new CatalogItem(request.CatalogTypeId, request.CatalogBrandId, request.Description, request.Name, request.Price, request.PictureUri);

            newItem = await _itemRepository.AddAsync(newItem, cancellationToken);

            if (newItem.Id != 0)
            {
                //We disabled the upload functionality and added a default/placeholder image to this sample due to a potential security risk 
                //  pointed out by the community. More info in this issue: https://github.com/dotnet-architecture/eShopOnWeb/issues/537 
                //  In production, we recommend uploading to a blob storage and deliver the image via CDN after a verification process.

                newItem.UpdatePictureUri("eCatalog-item-default.png");
                await _itemRepository.UpdateAsync(newItem, cancellationToken);
            }

            var dto = new CatalogItemDto
            {
                Id = newItem.Id,
                CatalogBrandId = newItem.CatalogBrandId,
                CatalogTypeId = newItem.CatalogTypeId,
                Description = newItem.Description,
                Name = newItem.Name,
                PictureUri = _uriComposer.ComposePicUri(newItem.PictureUri),
                Price = newItem.Price
            };
            response.CatalogItem = dto;
            return response;
        }


    }
}
