using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EditCatalogItemModel : PageModel
    {
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;

        public EditCatalogItemModel(IAsyncRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public async Task OnGet(CatalogItemViewModel catalogModel)
        {
            CatalogModel = catalogModel;

            var catalogItem = await _catalogItemRepository.GetByIdAsync(catalogModel.Id);
            catalogItem.Name = "New Name";
            
            await _catalogItemRepository.UpdateAsync(catalogItem);
            
            var catalogITem = await _catalogItemRepository.GetByIdAsync(catalogModel.Id);
        }
    }
}
