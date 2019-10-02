using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions.BasketLogicExceptions
{
    public class TooManyOfItemInBasketException : BasketLogicException
    {
        public TooManyOfItemInBasketException(int maxNumberOfItems)
            : base($"{nameof(BasketLogicException)} - cannot have more than {maxNumberOfItems} of the same type of item in the basket. Basket not updated.")
        {
        }

        protected TooManyOfItemInBasketException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public TooManyOfItemInBasketException(string message) : base(message)
        {
        }

        public TooManyOfItemInBasketException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
