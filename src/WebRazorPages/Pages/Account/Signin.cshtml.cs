using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using System;

namespace Microsoft.eShopWeb.RazorPages.Pages.Account
{
    public class SigninModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;


        public SigninModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
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
        public async Task OnPost()
        {

        }

        public async Task OnPostSignout()
        {
            await _signInManager.SignOutAsync();

            RedirectToPage("/Index");
        }
    }
}
