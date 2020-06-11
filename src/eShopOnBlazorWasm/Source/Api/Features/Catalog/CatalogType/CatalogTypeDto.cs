namespace eShopOnBlazorWasm.Features.Catalogs
{
  public class CatalogTypeDto
  {
    /// <summary>
    /// Catalog Type
    /// </summary>
    /// <example> </example>
    public string Type { get; set; }

    public CatalogTypeDto() { }
    public CatalogTypeDto(string aType) : this()
    {
      Type = aType;
    }
  }
}
