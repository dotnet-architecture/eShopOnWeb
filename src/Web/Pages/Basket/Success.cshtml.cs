using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class SuccessModel : PageBase
{
    
    public SuccessModel(IPublishEventService publishEventService) : base(publishEventService)
    {
    }

    public void OnGet()
    {
    }
}
