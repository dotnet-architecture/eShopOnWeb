using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Product;

public class IndexModel : PageModel
{
    private readonly ICatalogViewModelService _catalogViewModelService;

    public IndexModel(ICatalogViewModelService catalogViewModelService)
    {
        _catalogViewModelService = catalogViewModelService;
    }

    public CatalogItemViewModel CatalogItemModel { get; set; } = new CatalogItemViewModel();

    public async Task OnGet(CatalogIndexViewModel catalogModel, int? Id)
    {
        var CatalogModel = await _catalogViewModelService.GetCatalogItems(0, int.MaxValue, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        CatalogItemModel = CatalogModel.CatalogItems.Find(item => item.Id == Id);
    }
}
