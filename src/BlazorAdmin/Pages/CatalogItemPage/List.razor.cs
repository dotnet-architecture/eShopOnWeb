using BlazorAdmin.Helpers;
using BlazorAdmin.Services.CatalogItemServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.CatalogItemPage
{
    public partial class List : BlazorComponent
    {
        [Microsoft.AspNetCore.Components.Inject]
        public ListPaged ListPagedService { get; set; }

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
                catalogItems = await CatalogItemListPaged.HandleAsync(50);
                catalogTypes = await CatalogTypeService.List();
                catalogBrands = await CatalogBrandService.List();

                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async void DetailsClick(int id)
        {
            await DetailsComponent.Open(id);
        }

        private async Task CreateClick()
        {
            await CreateComponent.Open();
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
            catalogItems = await ListPagedService.HandleAsync(50);
            StateHasChanged();
        }
    }
}
