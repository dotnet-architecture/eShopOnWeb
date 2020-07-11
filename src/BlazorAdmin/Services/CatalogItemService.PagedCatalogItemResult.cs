using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services
{
    public class PagedCatalogItemResult
    {
        public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
        public int PageCount { get; set; } = 0;
    }
}
