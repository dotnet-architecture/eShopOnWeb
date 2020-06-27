using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.API.AuthEndpoints
{
    public class Authenticate : BaseAsyncEndpoint<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Authenticate(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
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

            return response;
        }
    }
}
