namespace Microsoft.eShopWeb.Web.ViewModels
{
    /// <summary>
    /// Represent the detail of pagination information
    /// </summary>
    public class PaginationInfoViewModel
    {
        /// <summary>
        /// Number of the total existing item
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Number of item by page
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// The number of the current page
        /// </summary>
        public int ActualPage { get; set; }

        /// <summary>
        /// The number of total page available
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Contains 'is-disabled' if there aren't previous page available
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// Contains 'is-disabled' if there aren't nex page available
        /// </summary>
        public string Next { get; set; }
    }
}
