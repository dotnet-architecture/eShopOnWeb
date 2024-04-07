using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.Orders;

public class GetOrdersList : IRequest<IEnumerable<OrderViewModel>>
{

}
