using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microsoft.eShopWeb.Web.ViewModels
{
	public class CatalogIndexViewModel
	{
		public IEnumerable<CatalogItemViewModel> CatalogItems { get; set; }
		public IEnumerable<SelectListItem> Brands { get; set; }
		public IEnumerable<SelectListItem> Types { get; set; }
		public int? BrandFilterApplied { get; set; }
		public int? TypesFilterApplied { get; set; }
		public PaginationInfoViewModel PaginationInfo { get; set; }
	}
}