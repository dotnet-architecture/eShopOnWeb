using Microsoft.eShopWeb.ViewModels;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetBasket(int basketId);
        Task<BasketViewModel> CreateBasket();
        Task<BasketViewModel> CreateBasketForUser(string userId);

        Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity);
    }
}
