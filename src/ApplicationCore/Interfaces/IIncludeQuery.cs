using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IIncludeQuery<TEntity, out TPreviousProperty>
    {
        Guid Id { get; }
        Dictionary<Guid, string> PathMap { get; }
        IncludeVisitor Visitor { get; }
        IEnumerable<string> Paths { get; }
    }
}
