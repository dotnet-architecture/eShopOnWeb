using BlazorShared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class ListPaged
    {
        private readonly HttpService _httpService;
        private readonly ICatalogBrandService _brandService;

        public ListPaged(HttpService httpService,
            ICatalogBrandService brandService)
        {
            _httpService = httpService;
            _brandService = brandService;
        }

        public async Task<List<CatalogItem>> HandleAsync(int pageSize)
        {
            var brands = await _brandService.List();
            var items = (await _httpService.HttpGet<PagedCatalogItemResult>($"catalog-items?PageSize={pageSize}")).CatalogItems;
            foreach(var item in items)
            {
                item.CatalogBrand = brands.FirstOrDefault(b => b.Id == item.CatalogBrandId).Name;
            }
            return items;
        }

    }
}