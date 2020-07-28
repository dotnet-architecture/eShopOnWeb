using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class WebFileSystem: IFileSystem
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public const string AUTH_KEY = "AuthKeyOfDoomThatMustBeAMinimumNumberOfBytes";

        public WebFileSystem(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("auth-key", AUTH_KEY);
        }

        public async Task<bool> SavePicture(string pictureName, string pictureBase64)
        {
            if (string.IsNullOrEmpty(pictureBase64) || !await UploadFile(pictureName, Convert.FromBase64String(pictureBase64)))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> UploadFile(string fileName, byte[] fileData)
        {
            if (!fileData.IsValidImage(fileName))
            {
                return false;
            }

            return await UploadToWeb(fileName, fileData);
        }

        private async Task<bool> UploadToWeb(string fileName, byte[] fileData)
        {
            var request = new FileItem
            {
                DataBase64 = Convert.ToBase64String(fileData), 
                FileName = fileName
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using var message = await _httpClient.PostAsync(_url, content);
            if (!message.IsSuccessStatusCode)
            {
                return false;
            }

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
