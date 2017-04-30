using ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(ApplicationUser user);
    }

    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
