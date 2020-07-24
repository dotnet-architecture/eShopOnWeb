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
        private bool showCreate = false;
        private bool showDetails = false;
        private bool showEdit = false;
        private bool showDelete = false;
        private int selectedId = 0;

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

        private void DetailsClick(int id)
        {
            selectedId = id;
            showDetails = true;
        }

        private void CreateClick()
        {
            showCreate = true;
        }

        private void EditClick(int id)
        {
            selectedId = id;
            showEdit = true;
        }

        private void DeleteClick(int id)
        {
            selectedId = id;
            showDelete = true;
        }

        private async Task CloseDetailsHandler(string action)
        {
            showDetails = false;
            await ReloadCatalogItems();
        }

        private void EditDetailsHandler(int id)
        {
            showDetails = false;
            selectedId = id;
            showEdit = true;
        }

        private async Task CloseEditHandler(string action)
        {
            showEdit = false;
            await ReloadCatalogItems();
        }

        private async Task CloseDeleteHandler(string action)
        {
            showDelete = false;
            await ReloadCatalogItems();
        }

        private async Task CloseCreateHandler(string action)
        {
            showCreate = false;
            await ReloadCatalogItems();
        }

        private async Task ReloadCatalogItems()
        {
            catalogItems = await new BlazorAdmin.Services.CatalogItemService.ListPaged(Auth).HandleAsync(50);
            StateHasChanged();
        }
    }
}
