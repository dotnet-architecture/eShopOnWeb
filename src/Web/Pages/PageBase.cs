using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages;

public abstract class PageBase : PageModel
{
    public PageBase(IPublishEventService publishEventService) 
    {
        publishEventService.PublishEvent(EventType.PageOpenings, new Dictionary<string, string> {{ "PageName", this.GetType().Name }});
    }
}
