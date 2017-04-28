using System;

namespace ApplicationCore.Exceptions
{
    public class CatalogImageMissingException : Exception
    {
        public CatalogImageMissingException(string message, 
            Exception innerException = null) 
            : base(message, innerException: innerException)
        {
        }
    }
}
