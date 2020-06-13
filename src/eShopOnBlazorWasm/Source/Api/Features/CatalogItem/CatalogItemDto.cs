namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using System;
  public class CatalogItemDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Uri PictureUri { get; set; }
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
