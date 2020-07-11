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

            catalogItems = (await Http.GetFromJsonAsync<PagedCatalogItemResult>($"{GeneralConstants.API_URL}catalog-items?PageSize=50")).CatalogItems;
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

        public class PagedCatalogItemResult
        {
            public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
            public int PageCount { get; set; } = 0;
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


        public class CatalogItem
        {
            public int Id { get; set; }
            public int CatalogTypeId { get; set; }
            public int CatalogBrandId { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string PictureUri { get; set; }
        }
    }
}
