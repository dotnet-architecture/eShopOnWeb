using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    // This class never gets called. Modify it based on your need.

    public class WebFileSystem : IFileSystem
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public const string AUTH_KEY = "<IMAGE_FILE_UPLOAD_API_KEY>";               

        public WebFileSystem(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("auth-key", AUTH_KEY);
        }

        public async Task<bool> SavePicture(string pictureName, string pictureBase64, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(pictureBase64) || !await UploadFile(pictureName, Convert.FromBase64String(pictureBase64), cancellationToken))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> UploadFile(string fileName, byte[] fileData, CancellationToken cancellationToken)
        {
            if (!fileData.IsValidImage(fileName))
            {
                return false;
            }

            return await UploadToWeb(fileName, fileData, cancellationToken);
        }

        private async Task<bool> UploadToWeb(string fileName, byte[] fileData, CancellationToken cancellationToken)
        {
            var request = new FileItem
            {
                DataBase64 = Convert.ToBase64String(fileData),
                FileName = fileName
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // TODO: Write the actual File image upload logic to web.            
            // Post this image binary content to an Image Upload API.

            return true;
        }
    }

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
}
