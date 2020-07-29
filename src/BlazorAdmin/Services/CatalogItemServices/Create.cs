using System.Threading.Tasks;

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
            return (await _authService.HttpPost<CreateCatalogItemResult>("catalog-items", catalogItem)).CatalogItem;
        }
    }
}
