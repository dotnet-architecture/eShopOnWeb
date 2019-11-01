using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}";
        private readonly IMemoryCache _cache;

        public IndexModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            var pageIndex = pageId ?? 0;
            var itemsPerPage = Constants.ITEMS_PER_PAGE;
            var brandFilterApplied = catalogModel.BrandFilterApplied;
            var typesFilterApplied = catalogModel.TypesFilterApplied;
            var cacheKey = string.Format(_itemsKeyTemplate, pageIndex, itemsPerPage, brandFilterApplied, typesFilterApplied);

            _cache.Remove(cacheKey);

            CatalogModel = await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPerPage, brandFilterApplied, typesFilterApplied);
        }
    }
}
