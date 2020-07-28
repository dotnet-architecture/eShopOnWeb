using System.Net;
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

            var result = await _authService.HttpPost("catalog-items", catalogItem);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonConvert.DeserializeObject<CreateCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
