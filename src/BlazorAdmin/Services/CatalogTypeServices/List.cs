using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorAdmin.Services.CatalogTypeServices
{
    public class List
    {
        private readonly AuthService _authService;

        public List(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<CatalogType>> HandleAsync()
        {
            var types = new List<CatalogType>();

            if (!_authService.IsLoggedIn)
            {
                return types;
            }

            try
            {
                var result = await _authService.HttpGet("catalog-types");
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return types;
                }

                types = JsonSerializer.Deserialize<CatalogTypeResult>(await result.Content.ReadAsStringAsync()).CatalogTypes;
            }
            catch (AccessTokenNotAvailableException)
            {
                return types;
            }

            return types;
        }

        public static string GetTypeName(IEnumerable<CatalogType> types, int typeId)
        {
            var type = types.FirstOrDefault(t => t.Id == typeId);

            return type == null ? "None" : type.Name;
        }

    }
}
