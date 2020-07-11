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
    public class CatalogBrandService
    {
        private readonly AuthService _authService;

        public CatalogBrandService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogBrand>> GetCatalogBrandsAsync()
        {
            var brands = new List<CatalogBrand>();
            if (!_authService.IsLoggedIn)
            {
                return brands;
            }

            try
            {
                var result = (await _authService.GetHttpClient().GetAsync($"{GeneralConstants.API_URL}catalog-brands"));
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return brands;
                }

                brands = JsonConvert.DeserializeObject<CatalogBrandResult>(await result.Content.ReadAsStringAsync()).CatalogBrands;
            }
            catch (AccessTokenNotAvailableException)
            {
                return brands;
            }

            return brands;
        }

    }
}
