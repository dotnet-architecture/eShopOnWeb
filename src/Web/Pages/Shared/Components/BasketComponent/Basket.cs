using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using BasketEntity = Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate.Basket;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Components.BasketComponent;

public class Basket : ViewComponent
{
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IReadRepository<BasketEntity> _basketRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Basket(IBasketViewModelService basketViewModelService,
        IReadRepository<BasketEntity> basketRepository,
        SignInManager<ApplicationUser> signInManager)
    {
        _basketViewModelService = basketViewModelService;
        _basketRepository = basketRepository;
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
            var userNameSpec = new BasketWithItemsSpecification(User.Identity.Name);
            
            var userBasket = await _basketRepository.GetBySpecAsync(userNameSpec);
            if (userBasket == null) return 0;
            return userBasket.TotalItems;
        }

        string anonymousId = GetAnnonymousIdFromCookie();
        if (anonymousId == null)
            return 0;

        var anonymousSpec = new BasketWithItemsSpecification(anonymousId);
        var anonymousBasket = await _basketRepository.GetBySpecAsync(anonymousSpec);
        if (anonymousBasket == null) return 0;
        return anonymousBasket.TotalItems;
    }

    private string GetAnnonymousIdFromCookie()
    {
        // TODO: Add a prefix to anonymous cookie values so they cannot collide with user names
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
