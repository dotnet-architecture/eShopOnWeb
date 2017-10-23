using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.RazorPages.Pages.Account
{
    public class SignoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
