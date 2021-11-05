using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException(int basketId) : base($"No basket found with id {basketId}")
    {
    }
}
