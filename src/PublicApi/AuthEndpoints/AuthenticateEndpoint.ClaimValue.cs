namespace Microsoft.eShopWeb.PublicApi.AuthEndpoints;

public class ClaimValue
{
    public ClaimValue()
    {
    }

    public ClaimValue(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
