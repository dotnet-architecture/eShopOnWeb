using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore;
public class ServiceBusSettings
{
    public string ConnectionString { get; set; }
    public string TopicName { get; set; }
}

