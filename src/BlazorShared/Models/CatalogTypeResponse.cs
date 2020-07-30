using System.Collections.Generic;

namespace BlazorShared.Models
{
    public class CatalogTypeResponse
    {
        public List<CatalogType> CatalogTypes { get; set; } = new List<CatalogType>();
    }
}
