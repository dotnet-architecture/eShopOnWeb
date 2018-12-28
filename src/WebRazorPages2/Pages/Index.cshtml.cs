using System.Threading.Tasks;
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

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            CatalogModel = await _catalogService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        }
    }
}
