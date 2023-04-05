using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Events;
public class OrderPaymentSucceeded
{
    public int OrderId { get; set; }
}
