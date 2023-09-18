using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.eShopWeb.Web.Pages.Payment
{
    public class ReturnModel : PageModel
    {
        private readonly CatalogContext _dbcontext;
        private readonly IMemoryCache _cache;

        public ReturnModel(CatalogContext dbContext,IMemoryCache cache)
        {
            _dbcontext = dbContext;
            _cache = cache;
        }
        public IActionResult OnGet(AlipayTradePayReturnResponse response)
        {
            long orderId = long.Parse(response.OutTradeNo);
            var order = _dbcontext.Orders.First(order => order.Id == orderId);
            order.Status = OrderConst.ORDER_STATIS_PURCHASED;
            _dbcontext.SaveChanges();

            bool isContextExist = _cache.TryGetValue($"httpcontext_identity_{orderId}", out ClaimsPrincipal user);
            if (isContextExist)
            {
                HttpContext.User = user;
                return RedirectToPage("/Basket/Success");
            }

            return RedirectToPage("Index");
        }
    }
}
