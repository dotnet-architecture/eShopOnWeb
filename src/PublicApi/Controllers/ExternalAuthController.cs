using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Azure.Core;
using Azure;
using Microsoft.eShopWeb.PublicApi.Models.ExternalAuth;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.PublicApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExternalAuthController : ControllerBase
{
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;

    public ExternalAuthController(ITokenClaimsService tokenClaimsService, SignInManager<ApplicationUser> signInManager, IUserService userService)
    {
        _tokenClaimsService = tokenClaimsService;
        _signInManager = signInManager;
        _userService = userService;
    }
    [HttpGet("google")]
    [AllowAnonymous]
    public IActionResult Index(string provider = "Google", string? returnUrl = null, string? tenant = "root")
    {
        // Construct the RedirectUri manually
        string callbackAction = "google-callback"; // Replace with your actual callback action name
        string controller = "ExternalAuth"; // Replace with your actual controller name

        string redirectUri = $"{Request.Scheme}://{Request.Host}/api/{controller}/{callbackAction}?ReturnUrl={returnUrl}&tenant={tenant}";


        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = redirectUri,
        };

        return Challenge(authenticationProperties, provider);
    }

    [HttpGet("google-callback")]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleCallback(string returnUrl = null)
    {
        try
        {
            string providerScheme = "Google";
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync(providerScheme);

            // TODO: Take all these to the userService

            if (!authenticateResult.Succeeded)
            {
                // Handle authentication failure
                throw new Exception("User Not Active. Please contact the administrator.");
            }

            string? email = authenticateResult?.Principal?.FindFirst(ClaimTypes.Email)?.Value;
            string? fullName = authenticateResult?.Principal?.FindFirst(ClaimTypes.Name)?.Value;
            string? imgURL = authenticateResult?.Principal?.FindFirstValue(ClaimTypes.UserData);


            //if no user with such an email exist create the user else get the JWT

            var user = await _userService.FindUserByUserNameAsync(email);

            if(user != null)
            {
                //get token of the email
                string token = await _tokenClaimsService.GetTokenAsync(email);
                StoreCookies(token);

                // User is successfully authenticated
                return Redirect(returnUrl);

               
            }

            //if it is null then register this user email
            int spaceIndex = fullName.IndexOf(' ');
            string firstName = fullName.Substring(0, spaceIndex);
            string lastName = fullName.Substring(spaceIndex + 1);

            //now register the user sin

            string userName = await _userService.RegisterUserAsync(email, firstName, lastName, imgURL);

            if (userName == null)
            {
                throw new Exception("couldn't register user");
            }

            //ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
            //if (info == null)
            //{
            //    throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            //}

            //await _userService.AddExternalLoginsAsync(user, info);





            //now get the token

            string tokenAfterRegistration = await _tokenClaimsService.GetTokenAsync(email);

            if (tokenAfterRegistration != null)
            {
                StoreCookies(tokenAfterRegistration);

                // User is successfully authenticated
                return Redirect(returnUrl);

            }


            // User is successfully authenticated
            return Redirect(returnUrl);

        }
        catch (Exception)
        {

            throw;
        }
    }

    private void StoreCookies(string cookieValue)
    {
        string cookieName = "jwt-object";
    

        int expirationDays = 7; // The number of days until the cookie expires

        CookieOptions option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(expirationDays)
        };

        Response.Cookies.Append(cookieName, cookieValue, option);
    }

}
