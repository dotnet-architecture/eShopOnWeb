using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Web.ViewModels;

public class AlipayTradePayReturnResponse
{
    [BindProperty(Name = "trade_no")]
    public string TradeNo { get; set; } = string.Empty;
    public string Charset { get; set; } = "UTF-8";
    [BindProperty(Name = "out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    [BindProperty(Name = "total_amount")]
    public decimal TotalAmount { get; set; }
    public string Sign { get; set; } = string.Empty;
    [BindProperty(Name = "auth_app_id")]
    public string AuthAppId { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0";
    [BindProperty(Name = "app_id")]
    public string AppId { get; set; } = string.Empty;
    [BindProperty(Name = "sign_type")]
    public string SignType { get; set; } = "RSA2";
    [BindProperty(Name = "seller_id")]
    public string SellerId { get; set; } = string.Empty;
    public DateTimeOffset TimeStamp { get; set; }
}
