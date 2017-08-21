using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using System;
using Web;

namespace Microsoft.eShopWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private const string _basketSessionKey = "basketId";
        private readonly IUriComposer _uriComposer;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public BasketController(IBasketService basketService,
            IUriComposer uriComposer,
            SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService;
            _uriComposer = uriComposer;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basketModel = await GetBasketViewModelAsync();

            return View(basketModel);
        }

        // POST: /Basket/AddToBasket
        [HttpPost]
        public async Task<IActionResult> AddToBasket(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToAction("Index", "Catalog");
            }
            var basketViewModel = await GetBasketViewModelAsync();

            await _basketService.AddItemToBasket(basketViewModel.Id, productDetails.Id, productDetails.Price, 1);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var basket = await GetBasketViewModelAsync();

            await _basketService.Checkout(basket.Id);

            return View("Checkout");
        }

        private async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await _basketService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            string anonymousId = GetOrSetBasketCookie();
            return await _basketService.GetOrCreateBasketForUser(anonymousId);
        }

        private string GetOrSetBasketCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            string anonymousId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions);
            return anonymousId;
        }
    }
}
