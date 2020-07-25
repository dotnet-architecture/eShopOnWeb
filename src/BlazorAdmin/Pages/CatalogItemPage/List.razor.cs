using BlazorAdmin.Helpers;
using BlazorAdmin.Services.CatalogBrandService;
using BlazorAdmin.Services.CatalogItemService;
using BlazorAdmin.Services.CatalogTypeService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAdmin.Pages.CatalogItemPage
{
    public partial class List : BlazorComponent
    {
        private List<CatalogItem> catalogItems = new List<CatalogItem>();
        private List<CatalogType> catalogTypes = new List<CatalogType>();
        private List<CatalogBrand> catalogBrands = new List<CatalogBrand>();

        private Edit EditComponent { get; set; }
        private Delete DeleteComponent { get; set; }
        private Details DetailsComponent { get; set; }
        private Create CreateComponent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                catalogItems = await new BlazorAdmin.Services.CatalogItemService.ListPaged(Auth).HandleAsync(50);
                catalogTypes = await new BlazorAdmin.Services.CatalogTypeService.List(Auth).HandleAsync();
                catalogBrands = await new BlazorAdmin.Services.CatalogBrandService.List(Auth).HandleAsync();

                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async void DetailsClick(int id)
        {
            await DetailsComponent.Open(id);
        }

        private void CreateClick()
        {
            CreateComponent.Open();
        }

        private async Task EditClick(int id)
        {
            await EditComponent.Open(id);
        }

        private async Task DeleteClick(int id)
        {
            await DeleteComponent.Open(id);
        }

        private async Task ReloadCatalogItems()
        {
            catalogItems = await new BlazorAdmin.Services.CatalogItemService.ListPaged(Auth).HandleAsync(50);
            StateHasChanged();
        }
    }
}
