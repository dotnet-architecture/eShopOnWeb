namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  public class CatalogBrandDto
  {
    /// <summary>
    /// The Brand Name
    /// </summary>
    /// <example>Azure</example>
    public string Brand { get; set; }

    public int Id { get; set; }

    public CatalogBrandDto() { }

    public CatalogBrandDto(int aId, string aBrand) : this()
    {
      Id = aId;
      Brand = aBrand;
    }
  }
}
