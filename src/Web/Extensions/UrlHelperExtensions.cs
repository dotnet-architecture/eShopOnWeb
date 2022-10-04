namespace Microsoft.AspNetCore.Mvc;

public static class UrlHelperExtensions
{
    public static string? EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
    {
        return urlHelper.Action(
            action: "GET",
            controller: "ConfirmEmail",
            values: new { userId, code },
            protocol: scheme);
    }
}
