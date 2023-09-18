using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails;

public class GetOrderDetails : IRequest<OrderDetailViewModel>
{
    public string UserName { get; set; }
    public long OrderId { get; set; }

    public GetOrderDetails(string userName, long orderId)
    {
        UserName = userName;
        OrderId = orderId;
    }
}
