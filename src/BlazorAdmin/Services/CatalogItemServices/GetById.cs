using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class GetById
    {
        private readonly AuthService _authService;

        public GetById(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<CatalogItem> HandleAsync(int catalogItemId)
        {
            var catalogItemResult = new CatalogItem();

            var result = await _authService.HttpGet($"catalog-items/{catalogItemId}");
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonSerializer.Deserialize<EditCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
