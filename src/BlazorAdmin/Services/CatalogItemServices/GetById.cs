using BlazorShared.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class GetById
    {
        private readonly HttpService _httpService;
        private readonly ICatalogBrandService _brandService;
        private readonly ICatalogTypeService _typeService;

        public GetById(HttpService httpService,
                        ICatalogBrandService brandService,
                        ICatalogTypeService typeService)
        {
            _brandService = brandService;
            _typeService = typeService;
            _httpService = httpService;
        }

        public async Task<CatalogItem> HandleAsync(int catalogItemId)
        {
            var brandListTask = _brandService.List();
            var typeListTask = _typeService.List();
            var itemGetTask = _httpService.HttpGet<EditCatalogItemResult>($"catalog-items/{catalogItemId}");
            await Task.WhenAll(brandListTask, typeListTask, itemGetTask);
            var brands = brandListTask.Result;
            var types = typeListTask.Result;
            var catalogItem = itemGetTask.Result.CatalogItem;
            catalogItem.CatalogBrand = brands.FirstOrDefault(b => b.Id == catalogItem.CatalogBrandId).Name;
            catalogItem.CatalogType = types.FirstOrDefault(t => t.Id == catalogItem.CatalogTypeId).Name;
            return catalogItem;
        }
    }
}
