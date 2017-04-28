using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Infrastructure.FileSystem
{
    public class LocalFileImageService : IImageService
    {
        private readonly IHostingEnvironment _env;

        public LocalFileImageService(IHostingEnvironment env)
        {
            _env = env;
        }
        public byte[] GetImageBytesById(int id)
        {
            var contentRoot = _env.ContentRootPath + "//Pics";
            var path = Path.Combine(contentRoot, id + ".png");
            return File.ReadAllBytes(path);
        }
    }
}
