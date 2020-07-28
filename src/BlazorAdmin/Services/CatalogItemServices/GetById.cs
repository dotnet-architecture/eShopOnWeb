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
            return (await _authService.HttpGet<EditCatalogItemResult>($"catalog-items/{catalogItemId}")).CatalogItem;
        }
    }
}
