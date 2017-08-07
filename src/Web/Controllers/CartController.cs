using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.ViewModels;
using System.Linq;

namespace Microsoft.eShopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        //private readonly IIdentityParser<ApplicationUser> _appUserParser;
        private const string _basketSessionKey = "basketId";
        private readonly IUriComposer _uriComposer;

        public CartController(IBasketService basketService,
            IUriComposer uriComposer)
//            IIdentityParser<ApplicationUser> appUserParser)
        {
            _basketService = basketService;
            _uriComposer = uriComposer;
            //          _appUserParser = appUserParser;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //var user = _appUserParser.Parse(HttpContext.User);
            var basketModel = await GetBasketFromSessionAsync();


            return View(basketModel);
        }

        // GET: /Cart/AddToCart
        // TODO: This should be a POST.
        public async Task<IActionResult> AddToCart(CatalogItem productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToAction("Index", "Catalog");
            }
            var basket = await GetBasketFromSessionAsync();

            await _basketService.AddItemToBasket(basket.Id, productDetails.Id, productDetails.Price, 1);

            return RedirectToAction("Index");
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
