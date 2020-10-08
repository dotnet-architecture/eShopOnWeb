using BlazorShared;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace BlazorAdmin.Services
{
    public class CatalogBrandService : ICatalogBrandService
    {
        // TODO: Make a generic service for any LookupData type
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogBrandService> _logger;
        private string _apiUrl;

        public CatalogBrandService(HttpClient httpClient,
            BaseUrlConfiguration baseUrlConfiguration,
            ILogger<CatalogBrandService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = baseUrlConfiguration.ApiBase;
        }

        public async Task<CatalogBrand> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogBrand>> List()
        {
            _logger.LogInformation("Fetching brands from API.");
            return (await _httpClient.GetFromJsonAsync<CatalogBrandResponse>($"{_apiUrl}catalog-brands"))?.CatalogBrands;
        }
    }
}
