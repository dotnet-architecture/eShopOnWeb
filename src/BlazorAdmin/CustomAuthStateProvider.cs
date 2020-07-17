using BlazorAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorAdmin
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;

        public CustomAuthStateProvider(AuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtString = await _authService.GetToken();
            var claims = _authService.ParseClaimsFromJwt(jwtString);

            var identity = new ClaimsIdentity(claims);

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
