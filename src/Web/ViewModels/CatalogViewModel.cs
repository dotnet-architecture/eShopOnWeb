using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.ViewModels
{
    public class CatalogViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<CatalogItem> Data { get; set; }
        public object PaginationInfo { get; internal set; }
    }
}
