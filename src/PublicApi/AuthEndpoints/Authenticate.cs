using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.AuthEndpoints
{
    public class Authenticate : BaseAsyncEndpoint<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public Authenticate(SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("api/authenticate")]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "auth.authenticate",
            Tags = new[] { "AuthEndpoints" })
        ]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request)
        {
            var response = new AuthenticateResponse(request.CorrelationId());

            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

            response.Result = result.Succeeded;

            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);

            return response;
        }
    }
}
