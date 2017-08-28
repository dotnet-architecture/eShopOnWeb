using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
        Task TransferBasketAsync(string anonymousId, string userName);
        Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity);
        Task SetQuantities(int basketId, Dictionary<string, int> quantities);
        Task Checkout(int basketId);
    }
}
