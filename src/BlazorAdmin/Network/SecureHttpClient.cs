using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAdmin.Constants;
using static BlazorAdmin.Pages.Index;

namespace BlazorAdmin.Network
{
    public class SecureHttpClient
    {
        private readonly HttpClient client;

        public SecureHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public void SetToken(string token)
        {
            if (IsTokenAdded())
            {
                this.client.DefaultRequestHeaders.Remove("Authorization");
            }
            
            this.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<List<CatalogBrand>> GetCatalogBrandsAsync()
        {
            var brands = new List<CatalogBrand>();
            if (!IsTokenAdded())
            {
                return brands;
            }

            try
            {
                brands = (await client.GetFromJsonAsync<CatalogBrandResult>($"{GeneralConstants.API_URL}catalog-brands")).CatalogBrands;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

            return brands;
        }

        private bool IsTokenAdded()
        {
            return this.client.DefaultRequestHeaders.Contains("Authorization");
        }
    }
}
