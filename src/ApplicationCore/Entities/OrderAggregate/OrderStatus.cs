using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
public enum OrderStatus
{
    Pending,
    Shipped,
    Delivered,
    Cancelled
}
