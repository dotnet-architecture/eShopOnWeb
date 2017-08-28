using Microsoft.eShopWeb.ViewModels;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
        Task TransferBasketAsync(string anonymousId, string userName);
        Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity);
        Task Checkout(int basketId);
    }
}
