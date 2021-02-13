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
    public class CachedCatalogTypeServiceDecorator : ICatalogTypeService
    {
        // TODO: Make a generic decorator for any LookupData type
        private readonly ILocalStorageService _localStorageService;
        private readonly CatalogTypeService _catalogTypeService;
        private ILogger<CachedCatalogTypeServiceDecorator> _logger;

        public CachedCatalogTypeServiceDecorator(ILocalStorageService localStorageService,
            CatalogTypeService catalogTypeService,
            ILogger<CachedCatalogTypeServiceDecorator> logger)
        {
            _localStorageService = localStorageService;
            _catalogTypeService = catalogTypeService;
            _logger = logger;
        }

        public async Task<CatalogType> GetById(int id)
        {
            return (await List()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogType>> List()
        {
            string key = "types";
            var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CatalogType>>>(key);
            if (cacheEntry != null)
            {
                _logger.LogInformation("Loading types from local storage.");
                if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
                {
                    return cacheEntry.Value;
                }
                else
                {
                    _logger.LogInformation("Cache expired; removing types from local storage.");
                    await _localStorageService.RemoveItemAsync(key);
                }
            }

            var types = await _catalogTypeService.List();
            var entry = new CacheEntry<List<CatalogType>>(types);
            await _localStorageService.SetItemAsync(key, entry);
            return types;
        }
    }
}
