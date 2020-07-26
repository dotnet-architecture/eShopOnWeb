using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class GetById
    {
        private readonly AuthService _authService;

        public GetById(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<CatalogItem> HandleAsync(int catalogItemId)
        {
            var catalogItemResult = new CatalogItem();

            var result = await _authService.HttpGet($"catalog-items/{catalogItemId}");
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return catalogItemResult;
            }

            catalogItemResult = JsonConvert.DeserializeObject<EditCatalogItemResult>(await result.Content.ReadAsStringAsync()).CatalogItem;

            return catalogItemResult;
        }
    }
}
