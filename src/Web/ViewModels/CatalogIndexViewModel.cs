using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    /// <summary>
    /// Represent the model for the complete catalog
    /// </summary>
    public class CatalogIndexViewModel
    {
        /// <summary>
        /// The list of the items contained in the catalog
        /// </summary>
        public List<CatalogItemViewModel> CatalogItems { get; set; }

        /// <summary>
        /// All brands available
        /// </summary>
        public List<SelectListItem> Brands { get; set; }

        /// <summary>
        /// All types of catalog's item available
        /// </summary>
        public List<SelectListItem> Types { get; set; }

        /// <summary>
        /// The id of brand filtered
        /// </summary>
        public int? BrandFilterApplied { get; set; }

        /// <summary>
        /// The id of item type filtered
        /// </summary>
        public int? TypesFilterApplied { get; set; }

        /// <summary>
        /// Representation of pagination information
        /// </summary>
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
