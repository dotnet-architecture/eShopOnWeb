namespace Microsoft.eShopWeb.PublicApi.AuthEndpoints.Requests;

public class ExternalAuthRequest
{
    public string provider { get; set; }
    public string returnUrl { get; set; }

}
