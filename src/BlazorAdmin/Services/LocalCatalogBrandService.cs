using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorAdmin.Services
{
    public class LocalCatalogBrandService : ICatalogBrandService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly CatalogBrandService _catalogBrandService;
        private ILogger<LocalCatalogBrandService> _logger;

        public LocalCatalogBrandService(ILocalStorageService localStorageService,
            CatalogBrandService catalogBrandService,
            ILoggerFactory loggerFactory)
        {
            _localStorageService = localStorageService;
            _catalogBrandService = catalogBrandService;
            _logger = loggerFactory.CreateLogger<LocalCatalogBrandService>();

        }

        public async Task<CatalogBrand> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogBrand>> List()
        {
            string key = "brands";
            var brands = await _localStorageService.GetItemAsync<List<CatalogBrand>>(key);
            if (brands != null)
            {
                _logger.LogWarning("Loaded brands from local storage.");
                return brands;
            }

            _logger.LogWarning("Loading brands from web API.");
            brands = await _catalogBrandService.List();
            await _localStorageService.SetItemAsync(key, brands);
            return brands;
        }
    }
}
