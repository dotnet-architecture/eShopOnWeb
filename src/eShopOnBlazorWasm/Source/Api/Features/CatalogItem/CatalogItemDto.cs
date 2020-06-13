namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  public class CatalogItemDto
  {
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Uri PictureUri { get; private set; }
    public int CatalogTypeId { get; private set; }
    public int CatalogBrandId { get; private set; }

    public CatalogItemDto() { }

    public CatalogItemDto
    (
      int aCatalogTypeId, 
      int aCatalogBrandId, 
      string aDescription, 
      string aName, 
      decimal aPrice, 
      Uri aPictureUri
    )
    {
      CatalogTypeId = aCatalogTypeId;
      CatalogBrandId = aCatalogBrandId;
      Description = aDescription;
      Name = aName;
      Price = aPrice;
      PictureUri = aPictureUri;
    }
  }
}
