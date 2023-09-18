using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class PaymentService
{
    private readonly DefaultAopClient _client;

    public PaymentService(DefaultAopClient client)
    {
        _client = client;
    }
    public string CreatePayPage(string subject, string orderId, decimal totalAmount)
    {
        var request = new AlipayTradePagePayRequest();
        var requestModel = new AlipayTradePagePayModel()
        {
            Subject = subject,
            OutTradeNo = orderId,
            TotalAmount = totalAmount.ToString(),
            TimeExpire = DateTimeOffset.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss"),
            ProductCode = "FAST_INSTANT_TRADE_PAY",
        };

        request.SetNotifyUrl($"http://localhost:5000/Payment/Notify");
        request.SetReturnUrl($"http://localhost:5000/Payment/Return");
        request.SetBizModel(requestModel);
        var response = _client.pageExecute(request);

        return response.Body;
    }
}
