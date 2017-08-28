using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ViewModels;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketService _cartSvc;

        public Basket(IBasketService cartSvc) => _cartSvc = cartSvc;

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var vm = new BasketComponentViewModel();
            var itemsInCart = await ItemsInBasketAsync(userName);
            vm.ItemsCount = itemsInCart;
            return View(vm);
        }
        private async Task<int> ItemsInBasketAsync(string userName)
        {
            var basket = await _cartSvc.GetOrCreateBasketForUser(userName);
            return basket.Items.Count;
        }
    }
}
