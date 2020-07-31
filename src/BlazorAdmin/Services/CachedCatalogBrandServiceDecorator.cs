using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class CachedCatalogBrandServiceDecorator : ICatalogBrandService
    {
        // TODO: Make a generic decorator for any LookupData type
        private readonly ILocalStorageService _localStorageService;
        private readonly CatalogBrandService _catalogBrandService;
        private ILogger<CachedCatalogBrandServiceDecorator> _logger;

        public CachedCatalogBrandServiceDecorator(ILocalStorageService localStorageService,
            CatalogBrandService catalogBrandService,
            ILogger<CachedCatalogBrandServiceDecorator> logger)
        {
            _localStorageService = localStorageService;
            _catalogBrandService = catalogBrandService;
            _logger = logger;

        }

        public async Task<CatalogBrand> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogBrand>> List()
        {
            string key = "brands";
            var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CatalogBrand>>>(key);
            if (cacheEntry != null)
            {
                _logger.LogInformation("Loading brands from local storage.");
                // TODO: Get Default Cache Duration from Config
                if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
                {
                    return cacheEntry.Value;
                }
                else
                {
                    _logger.LogInformation("Cache expired; removing brands from local storage.");
                    await _localStorageService.RemoveItemAsync(key);
                }
            }

            var brands = await _catalogBrandService.List();
            var entry = new CacheEntry<List<CatalogBrand>>(brands);
            await _localStorageService.SetItemAsync(key, entry);
            return brands;
        }
    }
}
