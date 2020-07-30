using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class ListPaged
    {
        private readonly HttpService _httpService;

        public ListPaged(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<CatalogItem>> HandleAsync(int pageSize)
        {
            return (await _httpService.HttpGet<PagedCatalogItemResult>($"catalog-items?PageSize={pageSize}")).CatalogItems;
        }

    }
}