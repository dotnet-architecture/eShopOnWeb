using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.eShopWeb.Web.Pages
{
    public class RedirectToPayModel : PageModel
    {
        public string FormData { get; set; } = "Payment Page";
        public void OnGet(string formData)
        {
            FormData = formData;
        }
    }
}
