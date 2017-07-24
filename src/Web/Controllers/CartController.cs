using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace Microsoft.eShopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        //private readonly IIdentityParser<ApplicationUser> _appUserParser;
        private const string _basketSessionKey = "basketId";

        public CartController(IBasketService basketService)
//            IIdentityParser<ApplicationUser> appUserParser)
        {
            _basketService = basketService;
  //          _appUserParser = appUserParser;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //var user = _appUserParser.Parse(HttpContext.User);
            var basket = await GetBasketFromSessionAsync();

            return View(basket);
        }

        public async Task<IActionResult> AddToCart(CatalogItem productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToAction("Index", "Catalog");
            }
            var basket = await GetBasketFromSessionAsync();

            basket.AddItem(productDetails.Id, productDetails.Price, 1);

            await _basketService.UpdateBasket(basket);

            return RedirectToAction("Index");
        }

        private async Task<Basket> GetBasketFromSessionAsync()
        {
            string basketId = HttpContext.Session.GetString(_basketSessionKey);
            Basket basket = null;
            if (basketId == null)
            {
                basket = await _basketService.CreateBasketForUser(User.Identity.Name);
                HttpContext.Session.SetString(_basketSessionKey, basket.Id);
            }
            else
            {
                basket = await _basketService.GetBasket(basketId);
            }
            return basket;
        }

    }
}
