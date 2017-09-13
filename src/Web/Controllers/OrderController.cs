using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.eShopWeb.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        public OrderController() { }

        public async Task<IActionResult> Detail(string orderId)
        {
            //var user = _appUserParser.Parse(HttpContext.User);

            //var order = await _orderSvc.GetOrder(user, orderId);
            return View();
        }

        //public async Task<IActionResult> Index(Order item)
        public async Task<IActionResult> Index()
        {
            //var user = _appUserParser.Parse(HttpContext.User);
            //var vm = await _orderSvc.GetMyOrders(user);
            return View();
        }
    }
}
