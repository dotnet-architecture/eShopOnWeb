namespace BlazorAdmin.Services.CatalogItemService
{
    public class CreateCatalogItemRequest
    {
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string PictureUri { get; set; } = string.Empty;
    }
}
