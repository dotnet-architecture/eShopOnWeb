using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorAdmin.Network;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAdmin.Pages
{
    public partial class Index
    {
        [Inject] private AuthService Auth { get; set; }
        [Inject] protected HttpClient Http { get; set; }

        private List<CatalogItem> catalogItems = new List<CatalogItem>();
        private List<CatalogType> catalogTypes = new List<CatalogType>();
        private List<CatalogBrand> catalogBrands = new List<CatalogBrand>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            catalogItems = await new CatalogItemService(Auth).GetPagedCatalogItemsAsync(50);
            catalogTypes = await new CatalogTypeService(Auth).GetCatalogTypesAsync();
            catalogBrands = await new CatalogBrandService(Auth).GetCatalogBrandsAsync();
        }

        protected string GetTypeName(int typeId)
        {
            var type = catalogTypes.FirstOrDefault(t => t.Id == typeId);

            return type == null ? "None" : type.Name;
        }

        protected string GetBrandName(int brandId)
        {
            var brand = catalogBrands.FirstOrDefault(t => t.Id == brandId);

            return brand == null ? "None" : brand.Name;
        }

    }
}
