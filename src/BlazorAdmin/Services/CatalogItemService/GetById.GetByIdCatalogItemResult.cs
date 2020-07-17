using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemService
{
    public class GetByIdCatalogItemResult
    {
        public CatalogItem CatalogItem { get; set; } = new CatalogItem();
    }
}
