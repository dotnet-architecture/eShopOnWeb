using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Task<int> CountTotalBasketItems(string username);
    }
}
