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
        private readonly IFileSystem _webFileSystem;

        public Create(IAsyncRepository<CatalogItem> itemRepository, IUriComposer uriComposer, IFileSystem webFileSystem)
        {
            _itemRepository = itemRepository;
            _uriComposer = uriComposer;
            _webFileSystem = webFileSystem;
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
                var picName = $"{newItem.Id}{Path.GetExtension(request.PictureName)}";
                if (await _webFileSystem.SavePicture(picName, request.PictureBase64, cancellationToken))
                {
                    newItem.UpdatePictureUri(picName);
                    await _itemRepository.UpdateAsync(newItem, cancellationToken);
                }
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
