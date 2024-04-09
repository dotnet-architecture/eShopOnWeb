using BlazorShared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorShared.Interfaces;
public interface IOrderService
{
    Task<string> EditAsync(OrderModel order);
    Task<List<OrderModel>> ListAsync();
}
