using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class GetById
    {
        private readonly HttpService _httpService;

        public GetById(HttpService httpService)
        {
            // _httpService = new HttpService(authService.GetHttpClient(), authService.ApiUrl);
            _httpService = httpService;
        }

        public async Task<CatalogItem> HandleAsync(int catalogItemId)
        {
            var catalogItem = (await _httpService.HttpGet<EditCatalogItemResult>($"catalog-items/{catalogItemId}")).CatalogItem;
            return catalogItem;
        }
    }
}
