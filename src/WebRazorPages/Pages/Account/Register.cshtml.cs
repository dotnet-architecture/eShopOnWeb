using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;

namespace Microsoft.eShopWeb.RazorPages.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(SignInManager<ApplicationUser> signInManager,
                        UserManager<ApplicationUser> userManager
)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel UserDetails { get; set; }


        public async Task<IActionResult> OnPost(string returnUrl = "/Index")
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = UserDetails.Email, Email = UserDetails.Email };
                var result = await _userManager.CreateAsync(user, UserDetails.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                AddErrors(result);
            }
            return Page();

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}
