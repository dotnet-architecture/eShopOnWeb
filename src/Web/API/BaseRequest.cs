using System;

namespace Microsoft.eShopWeb.Web.API
{
    /// <summary>
    /// Base class used by API requests
    /// </summary>
    public abstract class BaseMessage
    {
        /// <summary>
        /// Unique Identifier used by logging
        /// </summary>
        protected Guid _correlationId = Guid.NewGuid();
        public Guid CorrelationId() => _correlationId;
    }

    public abstract class BaseRequest : BaseMessage 
    {
    }
}
