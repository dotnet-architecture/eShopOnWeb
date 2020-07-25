using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class CreateCatalogItemResult
    {
        public CatalogItem CatalogItem { get; set; } = new CatalogItem();
    }
}
