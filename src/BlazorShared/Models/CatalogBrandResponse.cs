using BlazorShared.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorShared.Models
{
    public class CatalogBrandResponse : ILookupDataResponse<CatalogBrand>
    {
        [JsonPropertyName("CatalogBrands")]
        public List<CatalogBrand> List { get; set; } = new List<CatalogBrand>();
    }
}
