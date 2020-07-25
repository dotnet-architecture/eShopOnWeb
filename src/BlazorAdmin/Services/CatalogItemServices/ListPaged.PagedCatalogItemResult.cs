using System.Collections.Generic;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class PagedCatalogItemResult
    {
        public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
        public int PageCount { get; set; } = 0;
    }
}
