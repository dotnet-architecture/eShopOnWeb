using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Create
    {
        private readonly HttpService _httpService;

        public Create(AuthService authService)
        {
            _httpService = new HttpService(authService.GetHttpClient(), authService.ApiUrl);
        }

        public async Task<CatalogItem> HandleAsync(CreateCatalogItemRequest catalogItem)
        {
            return (await _httpService.HttpPost<CreateCatalogItemResult>("catalog-items", catalogItem)).CatalogItem;
        }
    }
}
