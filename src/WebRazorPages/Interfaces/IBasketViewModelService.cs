using Microsoft.eShopWeb.RazorPages.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.RazorPages.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
