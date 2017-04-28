using ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{

    public interface IImageService
    {
        byte[] GetImageBytesById(int id);
    }



}
