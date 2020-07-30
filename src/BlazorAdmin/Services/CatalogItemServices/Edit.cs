using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Edit
    {
        private readonly HttpService _httpService;

        public Edit(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CatalogItem> HandleAsync(CatalogItem catalogItem)
        {
            return (await _httpService.HttpPut<EditCatalogItemResult>("catalog-items", catalogItem)).CatalogItem;
        }
    }
}
