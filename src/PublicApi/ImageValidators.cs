using System;
using System.IO;

namespace Microsoft.eShopWeb.PublicApi;

public static class ImageValidators
{
    private const int ImageMaximumBytes = 512000;

    public static bool IsValidImage(this byte[] postedFile, string fileName)
    {
        return postedFile != null && postedFile.Length > 0 && postedFile.Length <= ImageMaximumBytes && IsExtensionValid(fileName);
    }

    private static bool IsExtensionValid(string fileName)
    {
        var extension = Path.GetExtension(fileName);

        return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
    }
}
