using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Edit
    {
        private readonly AuthService _authService;

        public Edit(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<CatalogItem> HandleAsync(CatalogItem catalogItem)
        {
            var catalogItemResult = new CatalogItem();

            var result = await _authService.HttpPut("catalog-items", catalogItem);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonSerializer.Deserialize<EditCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
