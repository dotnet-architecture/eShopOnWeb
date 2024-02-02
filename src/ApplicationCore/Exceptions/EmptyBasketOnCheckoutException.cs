using System;
namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    public class EmptyBasketOnCheckoutException : Exception
    {
        public EmptyBasketOnCheckoutException()
            : base($"Basket cannot have 0 items on checkout")
        {
        }

        public EmptyBasketOnCheckoutException(string message) : base(message)
        {
        }

        public EmptyBasketOnCheckoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
