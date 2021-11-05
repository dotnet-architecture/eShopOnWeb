using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message)
    {

    }

}
