using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class GetById
    {
        private readonly HttpService _httpService;

        public GetById(AuthService authService)
        {
            _httpService = new HttpService(authService.GetHttpClient(), authService.ApiUrl);
        }

        public async Task<CatalogItem> HandleAsync(int catalogItemId)
        {
            return (await _httpService.HttpGet<EditCatalogItemResult>($"catalog-items/{catalogItemId}")).CatalogItem;
        }
    }
}
