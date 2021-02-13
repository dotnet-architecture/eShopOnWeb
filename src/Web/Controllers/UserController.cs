using BlazorShared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenClaimsService _tokenClaimsService;

        public UserController(ITokenClaimsService tokenClaimsService)
        {
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentUser() =>
            Ok(User.Identity.IsAuthenticated ? await CreateUserInfo(User) : UserInfo.Anonymous);

        private async Task<UserInfo> CreateUserInfo(ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return UserInfo.Anonymous;
            }

            var userInfo = new UserInfo
            {
                IsAuthenticated = true
            };

            if (claimsPrincipal.Identity is ClaimsIdentity claimsIdentity)
            {
                userInfo.NameClaimType = claimsIdentity.NameClaimType;
                userInfo.RoleClaimType = claimsIdentity.RoleClaimType;
            }
            else
            {
                userInfo.NameClaimType = "name";
                userInfo.RoleClaimType = "role";
            }

            if (claimsPrincipal.Claims.Any())
            {
                var claims = new List<ClaimValue>();
                var nameClaims = claimsPrincipal.FindAll(userInfo.NameClaimType);
                foreach (var claim in nameClaims)
                {
                    claims.Add(new ClaimValue(userInfo.NameClaimType, claim.Value));
                }

                foreach (var claim in claimsPrincipal.Claims.Except(nameClaims))
                {
                    claims.Add(new ClaimValue(claim.Type, claim.Value));
                }

                userInfo.Claims = claims;
            }

            var token = await _tokenClaimsService.GetTokenAsync(claimsPrincipal.Identity.Name);
            userInfo.Token = token;

            return userInfo;
        }
    }
}