using BlazorShared;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace BlazorAdmin.Services
{
    // TODO: Implement timeout to periodically refresh from server
    public class CatalogBrandService : ICatalogBrandService
    {
        private readonly HttpClient _httpClient;
        private string _apiUrl;

        public CatalogBrandService(HttpClient httpClient,
            BaseUrlConfiguration baseUrlConfiguration)
        {
            _httpClient = httpClient;
            _apiUrl = baseUrlConfiguration.ApiBase;
        }

        public async Task<CatalogBrand> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogBrand>> List()
        {
            return (await _httpClient.GetFromJsonAsync<CatalogBrandResponse>($"{_apiUrl}catalog-brands"))?.CatalogBrands;
        }
    }
}
