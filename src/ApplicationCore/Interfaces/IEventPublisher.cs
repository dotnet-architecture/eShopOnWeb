using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;
public interface IEventPublisher<T> where T : class
{
    Task PublishEvent(T @event, CancellationToken cancellationToken = default);
}
