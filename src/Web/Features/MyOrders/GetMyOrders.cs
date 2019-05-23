using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.Features.MyOrders
{
    public class GetMyOrders : IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; set; }

        public GetMyOrders(string userName)
        {
            UserName = userName;
        }
    }
}
