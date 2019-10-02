using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions.BasketLogicExceptions
{
    public class BasketLogicException : Exception
    {
        public BasketLogicException()
            : base($"A {nameof(BasketLogicException)} was thrown.")
        {
        }

        protected BasketLogicException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public BasketLogicException(string message) : base(message)
        {
        }

        public BasketLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
