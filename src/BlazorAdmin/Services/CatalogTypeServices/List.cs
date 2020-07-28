using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogTypeServices
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

        public async Task<List<CatalogType>> HandleAsync()
        {
            return (await _httpClient.GetFromJsonAsync<CatalogTypeResult>($"{_authService.ApiUrl}catalog-types"))?.CatalogTypes;
        }

        public static string GetTypeName(IEnumerable<CatalogType> types, int typeId)
        {
            var type = types.FirstOrDefault(t => t.Id == typeId);

            return type == null ? "None" : type.Name;
        }

    }
}
