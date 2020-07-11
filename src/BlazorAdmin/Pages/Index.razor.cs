using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAdmin.Constants;
using BlazorAdmin.Network;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAdmin.Pages
{
    public partial class Index
    {
        [Inject] private AuthService Auth { get; set; }
        [Inject] protected HttpClient Http { get; set; }
        [Inject] private SecureHttpClient SecureHttp { get; set; }

        private List<CatalogItem> catalogItems = new List<CatalogItem>();
        private List<CatalogType> catalogTypes = new List<CatalogType>();
        private List<CatalogBrand> catalogBrands = new List<CatalogBrand>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            catalogItems = await new CatalogItemService(Auth).GetPagedCatalogItemsAsync(50);
            catalogTypes = (await Http.GetFromJsonAsync<CatalogTypeResult>($"{GeneralConstants.API_URL}catalog-types")).CatalogTypes;
            catalogBrands = await new CatalogBrandService(Auth).GetCatalogBrandsAsync();
            //catalogBrands = await SecureHttp.GetCatalogBrandsAsync();
        }

        protected string GetTypeName(int typeId)
        {
            return catalogTypes.FirstOrDefault(t => t.Id == typeId)?.Name;
        }

        protected string GetBrandName(int brandId)
        {
            var brand = catalogBrands.FirstOrDefault(t => t.Id == brandId);

            if (brand == null) return "None";
            return brand.Name;
        }

        public class CatalogTypeResult
        {
            public List<CatalogType> CatalogTypes { get; set; } = new List<CatalogType>();
        }

        public class CatalogType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }



    }
}
