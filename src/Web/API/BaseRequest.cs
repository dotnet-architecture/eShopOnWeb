using System;

namespace Microsoft.eShopWeb.Web.API
{
    /// <summary>
    /// Base class used by API requests
    /// </summary>
    public abstract class BaseRequest
    {
        /// <summary>
        /// Unique Identifier used by logging
        /// </summary>
        public Guid CorrelationId { get; set; }

        public BaseRequest()
        {
            CorrelationId = Guid.NewGuid();
        }
    }
}
