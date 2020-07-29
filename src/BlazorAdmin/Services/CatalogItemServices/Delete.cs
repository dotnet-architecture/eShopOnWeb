using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Delete
    {
        private readonly HttpService _httpService;

        public Delete(AuthService authService)
        {
            _httpService = new HttpService(authService.GetHttpClient(), authService.ApiUrl);
        }

        public async Task<string> HandleAsync(int catalogItemId)
        {
            return (await _httpService.HttpDelete<DeleteCatalogItemResult>("catalog-items", catalogItemId)).Status;
        }
    }
}
