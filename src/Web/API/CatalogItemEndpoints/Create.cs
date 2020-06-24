using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class Create : BaseAsyncEndpoint<CreateCatalogItemRequest, CreateCatalogItemResponse>
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public Create(IAsyncRepository<CatalogItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public override async Task<ActionResult<CreateCatalogItemResponse>> HandleAsync(CreateCatalogItemRequest request)
        {
            var response = new CreateCatalogItemResponse(request.CorrelationId);

            CatalogItem newItem = new CatalogItem(request.CatalogTypeId, request.CatalogBrandId, request.Description, request.Name, request.Price, request.PictureUri);

            newItem = await _itemRepository.AddAsync(newItem);

            var dto = new CatalogItemDto
            {
                CatalogBrandId = newItem.CatalogBrandId,
                CatalogTypeId = newItem.CatalogTypeId,
                Description = newItem.Description,
                Name = newItem.Name,
                PictureUri = newItem.PictureUri,
                Price = newItem.Price
            };
            response.CatalogItem = dto;
            return response;
        }
    }
}
