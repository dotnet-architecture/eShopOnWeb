using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Configuration;

//TODO : replace IMemoryCache with a distributed cache if you are in multi-host scenario
public class RevokeAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;

    public RevokeAuthenticationEvents(IMemoryCache cache, ILogger<RevokeAuthenticationEvents> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userId = context.Principal.Claims.First(c => c.Type == ClaimTypes.Name);
        var identityKey = context.Request.Cookies[ConfigureCookieSettings.IdentifierCookieName];

        if (_cache.TryGetValue($"{userId.Value}:{identityKey}", out var revokeKeys))
        {
            _logger.LogDebug($"Access has been revoked for: {userId.Value}.");
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
