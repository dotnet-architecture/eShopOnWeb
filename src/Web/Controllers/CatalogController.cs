using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

        // GET: /<controller>/
        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;           
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return View(catalogModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
