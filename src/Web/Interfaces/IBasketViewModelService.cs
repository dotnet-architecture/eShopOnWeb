using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUserAsync(string userName);
    Task<int> CountTotalBasketItemsAsync(string username);
    Task<BasketViewModel> MapAsync(Basket basket);
}
