using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class CachedCatalogItemServiceDecorator : ICatalogItemService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly CatalogItemService _catalogItemService;
    private ILogger<CachedCatalogItemServiceDecorator> _logger;

    public CachedCatalogItemServiceDecorator(ILocalStorageService localStorageService,
        CatalogItemService catalogItemService,
        ILogger<CachedCatalogItemServiceDecorator> logger)
    {
        _localStorageService = localStorageService;
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public async Task<List<CatalogItem>> ListPagedAsync(int pageSize)
    {
        string key = "items";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CatalogItem>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading items from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _catalogItemService.ListPagedAsync(pageSize);
        var entry = new CacheEntry<List<CatalogItem>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<List<CatalogItem>> ListAsync()
    {
        string key = "items";
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<CatalogItem>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading items from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _catalogItemService.ListAsync();
        var entry = new CacheEntry<List<CatalogItem>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<CatalogItem> GetByIdAsync(int id)
    {
        return (await ListAsync()).FirstOrDefault(x => x.Id == id);
    }

    public async Task<CatalogItem> CreateAsync(CreateCatalogItemRequest catalogItem)
    {
        var result = await _catalogItemService.CreateAsync(catalogItem);
        await RefreshLocalStorageListAsync();

        return result;
    }

    public async Task<CatalogItem> EditAsync(CatalogItem catalogItem)
    {
        var result = await _catalogItemService.EditAsync(catalogItem);
        await RefreshLocalStorageListAsync();

        return result;
    }

    public async Task<string> DeleteAsync(int id)
    {
        var result = await _catalogItemService.DeleteAsync(id);
        await RefreshLocalStorageListAsync();

        return result;
    }

    private async Task RefreshLocalStorageListAsync()
    {
        string key = "items";

        await _localStorageService.RemoveItemAsync(key);
        var items = await _catalogItemService.ListAsync();
        var entry = new CacheEntry<List<CatalogItem>>(items);
        await _localStorageService.SetItemAsync(key, entry);
    }
}
