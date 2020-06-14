namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  public class CatalogItemDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
#pragma warning disable CA1056 // Uri properties should not be strings
    public string PictureUriString { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }

    public CatalogItemDto() { }

    public CatalogItemDto
    (
      int aCatalogTypeId, 
      int aCatalogBrandId, 
      string aDescription, 
      string aName, 
      decimal aPrice,
#pragma warning disable CA1054 // Uri parameters should not be strings
      string aPictureUriString
#pragma warning restore CA1054 // Uri parameters should not be strings
    )
    {
      CatalogTypeId = aCatalogTypeId;
      CatalogBrandId = aCatalogBrandId;
      Description = aDescription;
      Name = aName;
      Price = aPrice;
      PictureUriString = aPictureUriString;
    }
  }
}
