using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage;

public class RemoveLoginViewModel
{
    [Required]
    public string LoginProvider { get; set; } = string.Empty;
    [Required]
    public string ProviderKey { get; set; } = string.Empty;
}
