using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Core;

namespace Microsoft.eShopWeb.Infrastructure.Data.Utils;
public static class CommonHelper
{
    private static readonly IdWorker _idWorker = new IdWorker(1, 1);
    public static long CreateOrderId()
    {
        return _idWorker.NextId();
    }
}
