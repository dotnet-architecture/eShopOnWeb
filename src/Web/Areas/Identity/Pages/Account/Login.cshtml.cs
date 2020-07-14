using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IBasketService _basketService;
        private readonly AuthService _authService;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, IBasketService basketService, AuthService authService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _basketService = basketService;
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _authService.LoginWithoutSaveToLocalStorage(new AuthRequest
                {
                    Username = Input.Email,
                    Password = Input.Password
                });

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, true);

                if (result.Result)
                {
                    CreateAuthCookie(result);
                    _logger.LogInformation("User logged in.");
                    await TransferAnonymousBasketToUserAsync(Input.Email);
                    return LocalRedirect("/admin");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void CreateAuthCookie(AuthResponse authResponse)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append("token", authResponse.Token, cookieOptions);
            Response.Cookies.Append("username", authResponse.Username, cookieOptions);
        }

        private async Task TransferAnonymousBasketToUserAsync(string userName)
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var anonymousId = Request.Cookies[Constants.BASKET_COOKIENAME];
                await _basketService.TransferBasketAsync(anonymousId, userName);
                Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }
        }
    }
}
