using System;

namespace ApplicationCore.Exceptions
{
    /// <summary>
    /// Note: No longer required.
    /// </summary>
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

        public CatalogImageMissingException() : base()
        {
        }

        public CatalogImageMissingException(string message) : base(message)
        {
        }
    }
}
