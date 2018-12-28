using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Basket(IBasketService basketService,
                        SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new BasketComponentViewModel();
            string userName = GetUsername();
            vm.ItemsCount = (await _basketService.GetBasketItemCountAsync(userName));
            return View(vm);
        }

        public class BasketComponentViewModel
        {
            public int ItemsCount { get; set; }
        }

        private string GetUsername()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return User.Identity.Name;
            }
            return GetBasketIdFromCookie() ?? Constants.DEFAULT_USERNAME;
        }

        private string GetBasketIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            return null;
        }
    }
}
