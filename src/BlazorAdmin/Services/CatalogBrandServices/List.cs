using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogBrandServices
{
    public class List
    {
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;

        public List(AuthService authService, HttpClient httpClient)
        {
            _authService = authService;
            _httpClient = httpClient;
        }

        public async Task<List<CatalogBrand>> HandleAsync()
        {
            return (await _httpClient.GetFromJsonAsync<CatalogBrandResult>($"{_authService.ApiUrl}catalog-brands"))?.CatalogBrands;
        }

        public static string GetBrandName(IEnumerable<CatalogBrand> brands, int brandId)
        {
            var type = brands.FirstOrDefault(t => t.Id == brandId);

            return type == null ? "None" : type.Name;
        }

    }
}
