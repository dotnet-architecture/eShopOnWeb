using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.eShopWeb.ApplicationCore.Enums;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.PublicApi.AuthEndpoints;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Microsoft.eShopWeb.PublicApi.RegistrationEndpoints;

public class RegistrationEndpoint : EndpointBaseAsync
    .WithRequest<RegistrationRequest>
    .WithActionResult<RegistrationResponse>
{
    private readonly IUserService _userService;

    public RegistrationEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("api/register")]
    [SwaggerOperation(
    Summary = "registers a user",
    Description = "registers a user",
    OperationId = "registration.register",
    Tags = new[] { "RegistrationEndpoints" })
]
    public override async Task<ActionResult<RegistrationResponse>> HandleAsync(RegistrationRequest request, CancellationToken cancellationToken = default)
    {
        //init AResponse
        AResponse<RegistrationResponse> aResponse = new AResponse<RegistrationResponse>();
        try
        {
          string username =  await _userService.RegisterUserAsync(request.Email, request.Password);

            RegistrationResponse registrationResponse = new RegistrationResponse(username);
           
            aResponse.Message = "User successfully registered";
            aResponse.Successful = true;
            aResponse.Data = registrationResponse;


        }
        catch (Exception ex)
        {
            aResponse.Message = ex.Message;
            aResponse.Successful = false;

        }
        return Ok(aResponse);

    }
}
