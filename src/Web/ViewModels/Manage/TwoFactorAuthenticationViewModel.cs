namespace Microsoft.eShopWeb.Web.ViewModels.Manage;

public class TwoFactorAuthenticationViewModel
{
    public bool HasAuthenticator { get; set; }
    public int RecoveryCodesLeft { get; set; }
    public bool Is2faEnabled { get; set; }
}
