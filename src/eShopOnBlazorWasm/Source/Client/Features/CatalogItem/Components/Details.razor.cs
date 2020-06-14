namespace eShopOnBlazorWasm.Features.CatalogItems.Components
{
  using Microsoft.AspNetCore.Components;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  public partial class Details
  {
    public CatalogItemDto CatalogItem => 
      CatalogItemState.CatalogItems.SingleOrDefault(aCatalogItemDto => aCatalogItemDto.Id == CatalogItemId);
    [Parameter] public int CatalogItemId { get; set; }

    public string CatalogBrand => CatalogBrandState.CatalogBrands[CatalogItem.CatalogBrandId].Brand;
    public string CatalogType => CatalogTypeState.CatalogTypes[CatalogItem.CatalogTypeId].Type;
  }
}
