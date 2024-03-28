using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface ICatalogItemService
{
    Task<CatalogItem> CreateAsync(CreateCatalogItemRequest catalogItem);
    Task<CatalogItem> EditAsync(CatalogItem catalogItem);
    Task<string> DeleteAsync(int id);
    Task<CatalogItem> GetByIdAsync(int id);
    Task<List<CatalogItem>> ListPagedAsync(int pageSize);
    Task<List<CatalogItem>> ListAsync();
}
