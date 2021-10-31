using BlazorShared;
using BlazorShared.Attributes;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class CatalogLookupDataService<TLookupData, TReponse>
        : ICatalogLookupDataService<TLookupData>
        where TLookupData : LookupData
        where TReponse : ILookupDataResponse<TLookupData>
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogLookupDataService<TLookupData, TReponse>> _logger;
        private readonly string _apiUrl;

        public CatalogLookupDataService(HttpClient httpClient,
            BaseUrlConfiguration baseUrlConfiguration,
            ILogger<CatalogLookupDataService<TLookupData, TReponse>> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = baseUrlConfiguration.ApiBase;
        }

        public async Task<List<TLookupData>> List()
        {
            var endpointName = typeof(TLookupData).GetCustomAttribute<EndpointAttribute>().Name;
            _logger.LogInformation($"Fetching {typeof(TLookupData).Name} from API. Enpoint : {endpointName}");

            var response = await _httpClient.GetFromJsonAsync<TReponse>($"{_apiUrl}{endpointName}");
            return response.List;
        }
    }
}
