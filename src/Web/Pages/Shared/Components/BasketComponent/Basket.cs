using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Components.BasketComponent;

public class Basket : ViewComponent
{
    private readonly IBasketViewModelService _basketService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Basket(IBasketViewModelService basketService,
                    SignInManager<ApplicationUser> signInManager)
    {
        _basketService = basketService;
        _signInManager = signInManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new BasketComponentViewModel
        {
            ItemsCount = await CountTotalBasketItems()
        };
        return View(vm);
    }

    private async Task<int> CountTotalBasketItems()
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            return await _basketService.CountTotalBasketItems(User.Identity.Name);
        }

        string anonymousId = GetAnnonymousIdFromCookie();
        if (anonymousId == null)
            return 0;

        return await _basketService.CountTotalBasketItems(anonymousId);
    }

    private string GetAnnonymousIdFromCookie()
    {
        if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
        {
            var id = Request.Cookies[Constants.BASKET_COOKIENAME];

            if (Guid.TryParse(id, out var _))
            {
                return id;
            }
        }
        return null;
    }
}
