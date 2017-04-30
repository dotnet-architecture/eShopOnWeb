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
        public CatalogImageMissingException(Exception innerException)
            : base("No catalog image found for the provided id.", 
                  innerException: innerException)
        {
        }
    }
}
