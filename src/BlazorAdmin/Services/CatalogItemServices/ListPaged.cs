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
            return (await _authService.HttpGet<PagedCatalogItemResult>($"catalog-items?PageSize={pageSize}")).CatalogItems;
        }

    }
}