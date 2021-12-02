using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<BasketViewModel> Map(Basket basket);
}
