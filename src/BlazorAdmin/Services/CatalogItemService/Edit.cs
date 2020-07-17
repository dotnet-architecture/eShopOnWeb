using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorAdmin.Services.CatalogItemService
{
    public class Edit
    {
        private readonly AuthService _authService;

        public Edit(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<CatalogItem> HandleAsync(CatalogItem catalogItem)
        {
            var catalogItemResult = new CatalogItem();

            var content = new StringContent(JsonConvert.SerializeObject(catalogItem), Encoding.UTF8, "application/json");

            var result = await _authService.GetHttpClient().PutAsync($"{Constants.API_URL}catalog-items", content);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonConvert.DeserializeObject<EditCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
