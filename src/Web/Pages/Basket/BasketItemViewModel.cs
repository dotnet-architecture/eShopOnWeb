using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class BasketItemViewModel
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }
  
    public string? PictureUrl { get; set; }
}
