using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    public class BasketLogicException : Exception
    {
        public BasketLogicException(int maxNumberOfItems)
            : base($"{nameof(BasketLogicException)} - cannot have more than {maxNumberOfItems} of the same type of item in the basket. Basket not updated.")
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
