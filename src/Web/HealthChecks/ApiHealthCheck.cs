using BlazorShared;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Web.HealthChecks;

public class ApiHealthCheck(IOptions<BaseUrlConfiguration> baseUrlConfiguration, IHttpClientFactory httpClientFactory) : IHealthCheck
{
    private readonly BaseUrlConfiguration _baseUrlConfiguration = baseUrlConfiguration.Value;
    private HttpClient? _httpClient;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        _httpClient = httpClientFactory.CreateClient();
        string myUrl = _baseUrlConfiguration.ApiBase + "catalog-items";
        var response = await _httpClient.GetAsync(myUrl, cancellationToken);
        var pageContents = await response.Content.ReadAsStringAsync(cancellationToken);
        if (pageContents.Contains(".NET Bot Black Sweatshirt"))
        {
            return HealthCheckResult.Healthy("The check indicates a healthy result.");
        }

        return HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
    }
}
