using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAdmin.Constants;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
                brands = (await _authService.GetHttpClient().GetFromJsonAsync<CatalogBrandResult>($"{GeneralConstants.API_URL}catalog-brands")).CatalogBrands;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

            return brands;
        }

    }
}
