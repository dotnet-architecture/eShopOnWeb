namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  public class CatalogBrandDto
  {
    /// <summary>
    /// The Brand Name
    /// </summary>
    /// <example>Azure</example>
    public string Brand { get; set; }

    public CatalogBrandDto() { }

    public CatalogBrandDto(string aBrand) : this()
    {
      Brand = aBrand;
    }
  }
}
