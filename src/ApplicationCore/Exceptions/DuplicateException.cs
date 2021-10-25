using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{

    public class DuplicateException : Exception
    {
        public DuplicateException(string message, int duplicateItemId) : base(message)
        {
            DuplicateItemId = duplicateItemId;
        }

        public int DuplicateItemId { get; }
    }
}
