namespace Microsoft.eShopWeb.Web.ViewModels
{
    /// <summary>
    /// Represent the detail of a catalog item
    /// </summary>
    public class CatalogItemViewModel
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path without domain of the item's picture
        /// </summary>
        public string PictureUri { get; set; }

        /// <summary>
        /// The price
        /// </summary>
        public decimal Price { get; set; }
    }
}
