using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;

namespace BlazorAdmin.Services
{
    public class CatalogTypeService
    {
        private readonly AuthService _authService;

        public CatalogTypeService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogType>> GetCatalogTypesAsync()
        {
            var types = new List<CatalogType>();

            if (!_authService.IsLoggedIn)
            {
                return types;
            }

            try
            {
                var result = (await _authService.GetHttpClient().GetAsync($"{Constants.API_URL}catalog-types"));
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return types;
                }

                types = JsonConvert.DeserializeObject<CatalogTypeResult>(await result.Content.ReadAsStringAsync()).CatalogTypes;
            }
            catch (AccessTokenNotAvailableException)
            {
                return types;
            }

            return types;
        }

    }
}
