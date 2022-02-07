using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

[Authorize]
public class SuccessModel : PageBase
{
    
    public SuccessModel(IPublishEventService publishEventService) : base(publishEventService)
    {
    }

    public void OnGet()
    {
    }
}
