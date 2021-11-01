using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {
        private readonly CatalogContext _dbContext;

        public BasketRepository(CatalogContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountTotalBasketItems(string username)
        {
            var totalItems = await _dbContext.Baskets
                .Where(basket => basket.BuyerId == username)
                .SelectMany(item => item.Items)
                .SumAsync(sum => sum.Quantity);

            return totalItems;
        }
    }
}
