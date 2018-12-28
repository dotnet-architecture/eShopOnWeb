using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.eShopWeb.RazorPages.Pages
{
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
