using System;
using System.Net.Http;
using System.Net.Http.Json;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.eShopWeb;
using Microsoft.Extensions.Logging;
using Shared.Authorization;

namespace BlazorAdmin
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);

        private readonly AuthService _authService;
        private readonly ILogger<CustomAuthStateProvider> _logger;

        private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(AuthService authService, ILogger<CustomAuthStateProvider> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() =>
            new AuthenticationState(await GetUser(useCache: true));

        private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = false)
        {
            var now = DateTimeOffset.Now;
            if (useCache && now < _userLastCheck + UserCacheRefreshInterval)
            {
                return _cachedUser;
            }

            _cachedUser = await FetchUser();
            _userLastCheck = now;

            return _cachedUser;
        }

        private async Task<ClaimsPrincipal> FetchUser()
        {
            UserInfo user = null;

            try
            {
                user = await _authService.GetTokenFromController();
            }
            catch (Exception exc)
            {
                _logger.LogWarning(exc, "Fetching user failed.");
            }
            
            if (user == null || !user.IsAuthenticated)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var identity = new ClaimsIdentity(
                nameof(CustomAuthStateProvider),
                user.NameClaimType,
                user.RoleClaimType);

            if (user.Claims != null)
            {
                foreach (var claim in user.Claims)
                {
                    identity.AddClaim(new Claim(claim.Type, claim.Value));
                }
            }

            return new ClaimsPrincipal(identity);
        }
    }
}
