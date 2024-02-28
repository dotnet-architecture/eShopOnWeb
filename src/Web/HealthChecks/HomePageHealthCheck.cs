using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.eShopWeb.Web.HealthChecks;

public class HomePageHealthCheck(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : IHealthCheck
{
    private HttpClient? _httpClient;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        _httpClient = httpClientFactory.CreateClient();
        var request = httpContextAccessor.HttpContext?.Request;
        string myUrl = request?.Scheme + "://" + request?.Host.ToString();
        var response = await _httpClient.GetAsync(myUrl, cancellationToken);
        var pageContents = await response.Content.ReadAsStringAsync(cancellationToken);
        if (pageContents.Contains(".NET Bot Black Sweatshirt"))
        {
            return HealthCheckResult.Healthy("The check indicates a healthy result.");
        }

        return HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
    }
}
