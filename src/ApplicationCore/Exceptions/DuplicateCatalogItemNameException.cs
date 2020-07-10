using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    public class DuplicateCatalogItemNameException : Exception
    {
        public DuplicateCatalogItemNameException(string message, int duplicateItemId) : base(message)
        {
            DuplicateItemId = duplicateItemId;
        }

        public int DuplicateItemId { get; }
    }
}
