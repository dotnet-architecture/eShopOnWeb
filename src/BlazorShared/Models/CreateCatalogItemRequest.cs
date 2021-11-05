using System.ComponentModel.DataAnnotations;

namespace BlazorShared.Models;

public class CreateCatalogItemRequest
{
    public int CatalogTypeId { get; set; }

    public int CatalogBrandId { get; set; }

    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; } = string.Empty;

    // decimal(18,2)
    [RegularExpression(@"^\d+(\.\d{0,2})*$", ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    [Range(0.01, 1000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } = 0;

    public string PictureUri { get; set; } = string.Empty;
    public string PictureBase64 { get; set; } = string.Empty;
    public string PictureName { get; set; } = string.Empty;

}
