using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class ListPaged
    {
        private readonly AuthService _authService;

        public ListPaged(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogItem>> HandleAsync(int pageSize)
        {
            var catalogItems = new List<CatalogItem>();

            var result = await _authService.HttpGet($"catalog-items?PageSize={pageSize}");
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItems;
            }

            catalogItems = JsonSerializer.Deserialize<PagedCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItems;

            return catalogItems;
        }

    }
}