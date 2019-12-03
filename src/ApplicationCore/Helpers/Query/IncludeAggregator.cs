using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Helpers.Query
{
    public class IncludeAggregator<TEntity>
    {
        public IncludeQuery<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> selector)
        {
            var visitor = new IncludeVisitor();
            visitor.Visit(selector);

            var id = Guid.NewGuid();
            var pathMap = new Dictionary<Guid, string>() { { id, visitor.Path } };

            return new IncludeQuery<TEntity, TProperty>(id, pathMap);
        }
    }
}
