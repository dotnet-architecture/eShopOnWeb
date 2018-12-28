using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.RazorPages.Pages.Account
{
    public class SigninModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketService _basketService;

        public SigninModel(SignInManager<ApplicationUser> signInManager,
            IBasketService basketService)
        {
            _signInManager = signInManager;
            _basketService = basketService;
        }

        [BindProperty]
        public LoginViewModel LoginDetails { get; set; } = new LoginViewModel();

        public class LoginViewModel
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


        public async Task OnGet(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            if (!String.IsNullOrEmpty(returnUrl) &&
                returnUrl.IndexOf("checkout", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                ViewData["ReturnUrl"] = "/Basket/Index";
            }
        }
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ViewData["ReturnUrl"] = returnUrl;

            var result = await _signInManager.PasswordSignInAsync(LoginDetails.Email, 
                LoginDetails.Password, LoginDetails.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                string anonymousBasketId = Request.Cookies[Constants.BASKET_COOKIENAME];
                if (!String.IsNullOrEmpty(anonymousBasketId))
                {
                    await _basketService.TransferBasketAsync(anonymousBasketId, LoginDetails.Email);
                    Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
                }
                return RedirectToPage(returnUrl ?? "/Index");
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = LoginDetails.RememberMe });
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
