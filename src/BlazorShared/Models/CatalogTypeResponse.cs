using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorShared.Interfaces;

namespace BlazorShared.Models;

public class CatalogTypeResponse : ILookupDataResponse<CatalogType>
{

    [JsonPropertyName("CatalogTypes")]
    public List<CatalogType> List { get; set; } = new List<CatalogType>();
}
