using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.eShopWeb.PublicApi
{
    public interface IFileSystem
    {
        Task<bool> UploadFile(string fileName, byte[] file);
    }
}
