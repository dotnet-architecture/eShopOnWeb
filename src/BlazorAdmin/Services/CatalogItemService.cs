using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAdmin.Constants;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using Index = BlazorAdmin.Pages.Index;

namespace BlazorAdmin.Services
{
    public class CatalogItemService
    {
        private readonly AuthService _authService;

        public CatalogItemService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogItem>> GetPagedCatalogItemsAsync(int pageSize)
        {
            var catalogItems = new List<CatalogItem>();

            var result = (await _authService.GetHttpClient().GetAsync($"{GeneralConstants.API_URL}catalog-items?PageSize={pageSize}"));
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItems;
            }

            catalogItems = JsonConvert.DeserializeObject<PagedCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItems;

            return catalogItems;
        }

    }
}