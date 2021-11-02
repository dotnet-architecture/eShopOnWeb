using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IBasketQueryService
    {
        Task<int> CountTotalBasketItems(string username);
    }
}
