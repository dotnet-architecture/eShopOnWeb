using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data.Utils;

namespace Microsoft.eShopWeb.Web.Controllers.Api;

public class TestpaymentController : BaseApiController
{
    private readonly PaymentService _paymentService;

    public TestpaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("testpay")]
    public IActionResult TestPay(string subject, decimal amount)
    {
        var id = CommonHelper.CreateOrderId().ToString();
        var response = _paymentService.CreatePayPage(subject, id, amount);

        return Ok();
    }
}
