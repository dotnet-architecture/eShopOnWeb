using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Helpers.Query
{
    public static class IncludeQueryExtensions
    {
        public static IIncludeQuery<TEntity, TNewProperty> Include<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, TPreviousProperty> query,
            Expression<Func<TEntity, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            var includeQuery = new IncludeQuery<TEntity, TNewProperty>(query.PathMap);
            query.PathMap[includeQuery] = query.Visitor.Path;

            return includeQuery;
        }

        public static IIncludeQuery<TEntity, TNewProperty> ThenInclude<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, TPreviousProperty> query,
            Expression<Func<TPreviousProperty, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            // If the visitor did not generated a path, return a new IncludeQuery with an unmodified PathMap.
            if (string.IsNullOrEmpty(query.Visitor.Path))
            {
                return new IncludeQuery<TEntity, TNewProperty>(query.PathMap);
            }

            var pathMap = query.PathMap;
            var existingPath = pathMap[query];
            pathMap.Remove(query);

            var includeQuery = new IncludeQuery<TEntity, TNewProperty>(query.PathMap);
            pathMap[includeQuery] = $"{existingPath}.{query.Visitor.Path}";

            return includeQuery;
        }

        public static IIncludeQuery<TEntity, TNewProperty> ThenInclude<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, IEnumerable<TPreviousProperty>> query,
            Expression<Func<TPreviousProperty, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            // If the visitor did not generated a path, return a new IncludeQuery with an unmodified PathMap.
            if (string.IsNullOrEmpty(query.Visitor.Path))
            {
                return new IncludeQuery<TEntity, TNewProperty>(query.PathMap);
            }

            var pathMap = query.PathMap;
            var existingPath = pathMap[query];
            pathMap.Remove(query);

            var includeQuery = new IncludeQuery<TEntity, TNewProperty>(query.PathMap);
            pathMap[includeQuery] = $"{existingPath}.{query.Visitor.Path}";

            return includeQuery;
        }
    }
}
