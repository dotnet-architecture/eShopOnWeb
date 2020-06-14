namespace eShopOnBlazorWasm.Features.CatalogTypes
{
  public class CatalogTypeDto
  {
    /// <summary>
    /// Catalog Type
    /// </summary>
    /// <example>T-Shirt</example>
    public string Type { get; set; }

    public int Id { get; set; }

    public CatalogTypeDto() { }
    public CatalogTypeDto(string aType) : this()
    {
      Type = aType;
    }
  }
}
