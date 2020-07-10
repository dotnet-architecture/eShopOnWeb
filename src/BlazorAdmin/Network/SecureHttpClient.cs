using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static BlazorAdmin.Pages.Index;

namespace BlazorAdmin.Network
{
    public class SecureHttpClient
    {
        private readonly HttpClient client;

        public SecureHttpClient(HttpClient client)
        {
            this.client = client;
            this.client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluQG1pY3Jvc29mdC5jb20iLCJyb2xlIjoiQWRtaW5pc3RyYXRvcnMiLCJuYmYiOjE1OTQ0MDQ2MjgsImV4cCI6MTU5NTAwOTQyOCwiaWF0IjoxNTk0NDA0NjI4fQ.10hYllCtfSQU3deYW0Slc7AHOS4QFD0yga_A9R_uuAY");
        }

        public async Task<List<CatalogBrand>> GetCatalogBrandsAsync()
        {
            var brands = new List<CatalogBrand>();

            try
            {
                brands = (await client.GetFromJsonAsync<CatalogBrandResult>("https://localhost:44339/api/catalog-brands")).CatalogBrands;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

            return brands;
        }
    }
}
