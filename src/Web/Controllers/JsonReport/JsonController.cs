using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.Web.Controllers.api;

[Route("api/report")]
public class JsonController : ControllerBase
{
    [HttpGet]
    public ActionResult GetJsonReport()
    {
        JsonReport report = new();
        report.TotalUser = 22;
        report.NumberOfOrder = 332;
        report.TotalPrice = 3323;
        report.TotalProduct = 33;
        return Ok(report);
    }
}

