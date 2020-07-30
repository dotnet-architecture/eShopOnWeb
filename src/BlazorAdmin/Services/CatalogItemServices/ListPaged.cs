using BlazorShared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class ListPaged
    {
        private readonly HttpService _httpService;
        private readonly ICatalogBrandService _brandService;
        private readonly ICatalogTypeService _typeService;

        public ListPaged(HttpService httpService,
            ICatalogBrandService brandService,
            ICatalogTypeService typeService)
        {
            _httpService = httpService;
            _brandService = brandService;
            _typeService = typeService;
        }

        public async Task<List<CatalogItem>> HandleAsync(int pageSize)
        {
            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemListTask = _httpService.HttpGet<PagedCatalogItemResult>($"catalog-items?PageSize={pageSize}");
            await Task.WhenAll(brandListTask, typeListTask, itemListTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var items = itemListTask.Result.CatalogItems;
            foreach(var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId).Name;
                item.CatalogType = types.FirstOrDefault(t => t.Id == item.CatalogTypeId).Name;
            }
            return items;
        }

    }
}