using System.ComponentModel.DataAnnotations;

namespace BlazorAdmin.Services.CatalogItemService
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        // decimal(18,2)
        [RegularExpression(@"^\d+(\.\d{0,2})*$", ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
        [Range(0, 9999999999999999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
