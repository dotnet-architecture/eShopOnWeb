using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.RazorPages.ViewModels;
using Microsoft.eShopWeb.RazorPages.Interfaces;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.RazorPages.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly IBasketService _basketService;
        private const string _basketSessionKey = "basketId";
        private readonly IUriComposer _uriComposer;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppLogger<IndexModel> _logger;
        private readonly IOrderService _orderService;

        public IndexModel(IBasketService basketService,
            IUriComposer uriComposer,
            SignInManager<ApplicationUser> signInManager,
            IAppLogger<IndexModel> logger,
            IOrderService orderService)
        {
            _basketService = basketService;
            _uriComposer = uriComposer;
            _signInManager = signInManager;
            _logger = logger;
            _orderService = orderService;
        }

        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();

        public async Task OnGet()
        {
            await SetBasketModelAsync();
        }

        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            await SetBasketModelAsync();

            await _basketService.AddItemToBasket(BasketModel.Id, productDetails.Id, productDetails.Price, 1);

            await SetBasketModelAsync();

            return Page();
        }

        public async Task OnPostUpdate(Dictionary<string,int> items)
        {
            await SetBasketModelAsync();
            await _basketService.SetQuantities(BasketModel.Id, items);

            await SetBasketModelAsync();
        }
        public async Task OnPostCheckout(Dictionary<string,int> items)
        {
            await SetBasketModelAsync();
            await _basketService.SetQuantities(BasketModel.Id, items);

            await _orderService.CreateOrderAsync(BasketModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));

            await _basketService.DeleteBasketAsync(BasketModel.Id);

            RedirectToPage("/Basket/Checkout");
        }

        private async Task SetBasketModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                BasketModel = await _basketService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                string anonymousId = GetOrSetBasketCookie();
                BasketModel = await _basketService.GetOrCreateBasketForUser(anonymousId);
            }
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
