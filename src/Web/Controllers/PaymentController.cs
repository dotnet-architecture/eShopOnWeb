using System.ComponentModel;
using System.Text.Json;
using Aop.Api.Domain;
using Aop.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers;
[Route("[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly CatalogContext _dbcontext;

    public PaymentController(CatalogContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    //public IActionResult Return(AlipayTradePayReturnResponse response)
    //{
        
    //    long orderId = long.Parse(response.OutTradeNo);
    //    var order = _dbcontext.Orders.First(order => order.Id == orderId);
    //    order.Status = OrderConst.ORDER_STATIS_PURCHASED;
    //    _dbcontext.SaveChanges();

    //    return RedirectToPage("/Basket/Success");
    //}

    public IActionResult Notify(long out_trade_no, string trade_status, DateTime gmt_payment)
    {
        var order = _dbcontext.Orders.First(order => order.Id == out_trade_no);
        if (order is null)
        {
            Response.WriteAsync("fail");
            return NotFound();
        }
        order.Status = OrderConst.ORDER_STATIS_PURCHASED;
        _dbcontext.SaveChanges();

        Response.WriteAsync("success");
        return Ok("Success");
    }
}
