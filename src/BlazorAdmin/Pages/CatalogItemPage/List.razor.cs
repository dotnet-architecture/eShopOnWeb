using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.CatalogItemPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public ICatalogItemService CatalogItemService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public ICatalogLookupDataService<CatalogBrand> CatalogBrandService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public ICatalogLookupDataService<CatalogType> CatalogTypeService { get; set; }

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
            catalogItems = await CatalogItemService.ListAsync();
            catalogTypes = await CatalogTypeService.ListAsync();
            catalogBrands = await CatalogBrandService.ListAsync();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClickAsync(int id)
    {
        await DetailsComponent.OpenAsync(id);
    }

    private async Task CreateClickAsync()
    {
        await CreateComponent.OpenAsync();
    }

    private async Task EditClickAsync(int id)
    {
        await EditComponent.OpenAsync(id);
    }

    private async Task DeleteClickAsync(int id)
    {
        await DeleteComponent.OpenAsync(id);
    }

    private async Task ReloadCatalogItemsAsync()
    {
        catalogItems = await CatalogItemService.ListAsync();
        StateHasChanged();
    }
}
