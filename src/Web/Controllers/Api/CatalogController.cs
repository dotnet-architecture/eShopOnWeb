using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    /// <summary>
    /// Controller to manage the catalog of product
    /// </summary>
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="catalogViewModelService"></param>
        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        /// <summary>
        /// Get all catalog item available (use pagination)
        /// </summary>
        /// <param name="brandFilterApplied">Filter by brand id</param>
        /// <param name="typesFilterApplied">Filter by type id</param>
        /// <param name="page">The page wants to load</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CatalogIndexViewModel), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }
    }
}
