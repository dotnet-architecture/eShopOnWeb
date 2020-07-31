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
    public class CatalogTypeService : ICatalogTypeService
    {
        // TODO: Make a generic service for any LookupData type
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogTypeService> _logger;
        private string _apiUrl;

        public CatalogTypeService(HttpClient httpClient,
            BaseUrlConfiguration baseUrlConfiguration,
            ILogger<CatalogTypeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = baseUrlConfiguration.ApiBase;
        }

        public async Task<CatalogType> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogType>> List()
        {
            _logger.LogInformation("Fetching types from API.");
            return (await _httpClient.GetFromJsonAsync<CatalogTypeResponse>($"{_apiUrl}catalog-types"))?.CatalogTypes;
        }
    }
}
