using BlazorShared;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorAdmin.Services
{
    public class CatalogItemService : ICatalogItemService
    {
        private readonly ICatalogBrandService _brandService;
        private readonly ICatalogTypeService _typeService;
        private readonly HttpService _httpService;
        private readonly ILogger<CatalogItemService> _logger;
        private string _apiUrl;

        public CatalogItemService(ICatalogBrandService brandService,
            ICatalogTypeService typeService,
            HttpService httpService,
            BaseUrlConfiguration baseUrlConfiguration,
            ILogger<CatalogItemService> logger)
        {
            _brandService = brandService;
            _typeService = typeService;

            _httpService = httpService;
            _logger = logger;
            _apiUrl = baseUrlConfiguration.ApiBase;
        }

        public async Task<CatalogItem> Create(CreateCatalogItemRequest catalogItem)
        {
            return (await _httpService.HttpPost<CreateCatalogItemResponse>("catalog-items", catalogItem)).CatalogItem;
        }

        public async Task<CatalogItem> Edit(CatalogItem catalogItem)
        {
            return (await _httpService.HttpPut<EditCatalogItemResult>("catalog-items", catalogItem)).CatalogItem;
        }

        public async Task<string> Delete(int catalogItemId)
        {
            return (await _httpService.HttpDelete<DeleteCatalogItemResponse>("catalog-items", catalogItemId)).Status;
        }

        public async Task<CatalogItem> GetById(int id)
        {
            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemGetTask = _httpService.HttpGet<EditCatalogItemResult>($"catalog-items/{id}");
            await Task.WhenAll(brandListTask, typeListTask, itemGetTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var catalogItem = itemGetTask.Result.CatalogItem;
            catalogItem.CatalogBrand = brands.FirstOrDefault(b => b.Id == catalogItem.CatalogBrandId)?.Name;
            catalogItem.CatalogType = types.FirstOrDefault(t => t.Id == catalogItem.CatalogTypeId)?.Name;
            return catalogItem;
        }

        public async Task<List<CatalogItem>> ListPaged(int pageSize)
        {
            _logger.LogInformation("Fetching catalog items from API.");

            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemListTask = _httpService.HttpGet<PagedCatalogItemResponse>($"catalog-items?PageSize=10");
            await Task.WhenAll(brandListTask, typeListTask, itemListTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var items = itemListTask.Result.CatalogItems;
            foreach (var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId)?.Name;
                item.CatalogType = types.FirstOrDefault(t => t.Id == item.CatalogTypeId)?.Name;
            }
            return items;
        }

        public async Task<List<CatalogItem>> List()
        {
            _logger.LogInformation("Fetching catalog items from API.");

            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            //TODO: Need to change the api to support full list
            var itemListTask = _httpService.HttpGet<PagedCatalogItemResponse>($"catalog-items?PageSize=100");
            await Task.WhenAll(brandListTask, typeListTask, itemListTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var items = itemListTask.Result.CatalogItems;
            foreach (var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId)?.Name;
                item.CatalogType = types.FirstOrDefault(t => t.Id == item.CatalogTypeId)?.Name;
            }
            return items;
        }
    }
}
