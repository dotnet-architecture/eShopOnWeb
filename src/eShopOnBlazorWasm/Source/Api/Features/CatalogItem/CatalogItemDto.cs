namespace eShopOnBlazorWasm.Features.CatalogItem
{
  using System;
  public class CatalogItemDto
  {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Uri PictureUri { get; private set; }
    public int CatalogTypeId { get; private set; }
    public int CatalogBrandId { get; private set; }

    public CatalogItemDto() { }

    public CatalogItemDto(int catalogTypeId, int catalogBrandId, string description, string name, decimal price, Uri pictureUri)
    {
      CatalogTypeId = catalogTypeId;
      CatalogBrandId = catalogBrandId;
      Description = description;
      Name = name;
      Price = price;
      PictureUri = pictureUri;
    }
  }
}
