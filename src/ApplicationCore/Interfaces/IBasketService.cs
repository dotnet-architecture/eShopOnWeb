using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(string basketId);
        Task<Basket> CreateBasket();
        Task<Basket> CreateBasketForUser(string userId);
        Task UpdateBasket(Basket basket);
    }
}
