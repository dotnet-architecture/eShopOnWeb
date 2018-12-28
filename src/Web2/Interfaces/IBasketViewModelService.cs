using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
