using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
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
            return (await _authService.HttpPut<EditCatalogItemResult>("catalog-items", catalogItem)).CatalogItem;
        }
    }
}
