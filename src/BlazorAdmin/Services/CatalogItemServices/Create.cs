using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Create
    {
        private readonly AuthService _authService;

        public Create(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<CatalogItem> HandleAsync(CreateCatalogItemRequest catalogItem)
        {
            var catalogItemResult = new CatalogItem();

            var content = new StringContent(JsonConvert.SerializeObject(catalogItem), Encoding.UTF8, "application/json");

            var result = await _authService.GetHttpClient().PostAsync($"{Constants.API_URL}catalog-items", content);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonConvert.DeserializeObject<CreateCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
