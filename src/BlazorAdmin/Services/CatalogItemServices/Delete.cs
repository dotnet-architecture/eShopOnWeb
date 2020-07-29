using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAdmin.Services.CatalogItemServices
{
    public class Delete
    {
        private readonly AuthService _authService;

        public Delete(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<string> HandleAsync(int catalogItemId)
        {
            return (await _authService.HttpDelete<DeleteCatalogItemResult>("catalog-items", catalogItemId)).Status;
        }
    }
}
