using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.eShopWeb.RazorPages.Interfaces;

namespace Microsoft.eShopWeb.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogService;

        public IndexModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [BindProperty]
        public CatalogIndexViewModel CatalogModel { get; set; }

        public async Task OnGet(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            CatalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
        }
    }
}
