using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;

namespace BlazorAdmin.Services.CatalogBrandService
{
    public class List
    {
        private readonly AuthService _authService;

        public List(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogBrand>> HandleAsync()
        {
            var brands = new List<CatalogBrand>();
            if (!_authService.IsLoggedIn)
            {
                return brands;
            }

            try
            {
                var result = (await _authService.GetHttpClient().GetAsync($"{Constants.API_URL}catalog-brands"));
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

        public static string GetBrandName(IEnumerable<CatalogBrand> brands, int brandId)
        {
            var type = brands.FirstOrDefault(t => t.Id == brandId);

            return type == null ? "None" : type.Name;
        }

    }
}
