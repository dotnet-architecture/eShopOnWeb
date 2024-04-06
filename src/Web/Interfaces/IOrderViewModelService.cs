
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IOrderViewModelService
{
    Task<List<OrderViewModel>> GetOrderList();
    Task<OrderViewModel> GetOrderById(int id);
    Task<List<OrderItemViewModel>> GetOrderItems(int orderId);
}
