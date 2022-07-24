using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class AzFuncAppClient : IAzFuncAppClient
{
    private readonly HttpClient _httpClient;

    public AzFuncAppClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task PostAsync(OrderDetailsProcessingDto model, CancellationToken cancellation = default)
    {
        var result = await _httpClient.PostAsJsonAsync("order-details", model, cancellation);
        result.EnsureSuccessStatusCode();
    }
}
