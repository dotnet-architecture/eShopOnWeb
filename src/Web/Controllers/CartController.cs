using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;

namespace Microsoft.eShopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ICatalogService _catalogSvc;
        private readonly IBasketService _basketSvc;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public CartController(IBasketService basketSvc,
            IIdentityParser<ApplicationUser> appUserParser)
        {
            //_catalogSvc = catalogSvc;
            _basketSvc = basketSvc;
            _appUserParser = appUserParser;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = _appUserParser.Parse(HttpContext.User);
            var viewmodel = await _basketSvc.GetBasket(user);

            return View(viewmodel);
        }

        // GET: /Cart/AddToCart
        // TODO: This should be a POST.
        public async Task<IActionResult> AddToCart(CatalogItem productDetails)
        {
            var user = _appUserParser.Parse(HttpContext.User);
            var product = new BasketItem()
            {
                Id = Guid.NewGuid().ToString(),
                Quantity = 1,
                UnitPrice = productDetails.Price,
                ProductId = productDetails.Id
            };
            // TODO: Save the item
            //await _basketSvc.AddItemToBasket(user, product);
            return RedirectToAction("Index", "Catalog");
        }
    }
}
