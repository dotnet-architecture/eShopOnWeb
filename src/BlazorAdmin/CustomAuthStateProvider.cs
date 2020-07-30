using BlazorAdmin.Services;
using BlazorShared.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorAdmin
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);

        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomAuthStateProvider> _logger;

        private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(AuthService authService,
            HttpClient httpClient, 
            ILogger<CustomAuthStateProvider> logger)
        {
            _authService = authService;
            _httpClient = httpClient;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _logger.LogWarning("Calling GetAuthenticationStateAsync");
            return new AuthenticationState(await GetUser(useCache: true));
        }

        private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = false)
        {
            // TODO: get token from User endpoint instead of from cookie at login
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
                user = await _httpClient.GetFromJsonAsync<UserInfo>("User");
            }
            catch (Exception exc)
            {
                _logger.LogWarning(exc, "Fetching user failed.");
            }
            
            if (user == null || !user.IsAuthenticated)
            {
                return null;
                //return new ClaimsPrincipal(new ClaimsIdentity());
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
