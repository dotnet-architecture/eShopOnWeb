using System;

namespace Microsoft.eShopWeb.Web.API
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Used to correlate request and response
        /// </summary>
        public Guid CorrelationId { get; set; }

        public BaseResponse(Guid correlationId) : base()
        {
            CorrelationId = correlationId;
        }

        public BaseResponse()
        {
        }
    }

}
