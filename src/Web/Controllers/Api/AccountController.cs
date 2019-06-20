using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.Infrastructure.Identity;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [HttpPost]
        public async void Register(string username, string password)
        {
            var user = new ApplicationUser { UserName = username, Email = username };
            await _userManager.CreateAsync(user, password);
        }
    }
}
