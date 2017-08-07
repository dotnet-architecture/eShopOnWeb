using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.ViewModels;

namespace Microsoft.eShopWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        private const string _basketSessionKey = "basketId";
        private readonly IUriComposer _uriComposer;

        public CartController(IBasketService basketService,
            IUriComposer uriComposer)
        {
            _basketService = basketService;
            _uriComposer = uriComposer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basketModel = await GetBasketFromSessionAsync();

            return View(basketModel);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToAction("Index", "Catalog");
            }
            var basket = await GetBasketFromSessionAsync();

            await _basketService.AddItemToBasket(basket.Id, productDetails.Id, productDetails.Price, 1);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var basket = await GetBasketFromSessionAsync();

            await _basketService.Checkout(basket.Id);

            return View("Checkout");
        }

        private async Task<BasketViewModel> GetBasketFromSessionAsync()
        {
            string basketId = HttpContext.Session.GetString(_basketSessionKey);
            BasketViewModel basket = null;
            if (basketId == null)
            {
                basket = await _basketService.CreateBasketForUser(User.Identity.Name);
                HttpContext.Session.SetString(_basketSessionKey, basket.Id.ToString());
            }
            else
            {
                basket = await _basketService.GetBasket(int.Parse(basketId));
            }
            return basket;
        }
    }
}
