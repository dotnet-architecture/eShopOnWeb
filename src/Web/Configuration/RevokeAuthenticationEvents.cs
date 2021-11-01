using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Configuration
{
    public class RevokeAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public RevokeAuthenticationEvents(IMemoryCache cache, ILogger<RevokeAuthenticationEvents> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userId = context.Principal.Claims.First(c => c.Type == ClaimTypes.Name);
            var identityKey = context.Request.Cookies[ConfigureCookieSettings.IdentifierCookieName];

            if (_cache.TryGetValue($"{userId.Value}:{identityKey}", out var revokeKeys))
            {
                _logger.LogDebug($"Access has been revoked for: {userId.Value}.");
                context.RejectPrincipal();
            }
            return Task.CompletedTask;
        }
    }
}
