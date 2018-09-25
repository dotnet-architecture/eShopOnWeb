using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
	public interface IOrderRepository : IRepository<Order>, IAsyncRepository<Order>
	{
		Order GetByIdWithItems(int id);

		Task<Order> GetByIdWithItemsAsync(int id);
	}
}