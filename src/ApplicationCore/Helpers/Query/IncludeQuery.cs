using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Helpers.Query
{
    public class IncludeQuery<TEntity> : IIncludeQuery<TEntity>
    {
        public Guid Id { get; }
        public Dictionary<Guid, string> PathMap { get; } = new Dictionary<Guid, string>();
        public IncludeVisitor Visitor { get; } = new IncludeVisitor();

        public IncludeQuery(Guid id, Dictionary<Guid, string> pathMap)
        {
            Id = id;
            PathMap = pathMap;
        }

        public IEnumerable<string> Paths => PathMap.Select(x => x.Value).Distinct().ToList();
    }

    public class IncludeQuery<TEntity, TPreviousProperty> : IncludeQuery<TEntity>, IIncludeQuery<TEntity, TPreviousProperty>
    {
        public IncludeQuery(Guid id, Dictionary<Guid, string> pathMap) : base(id, pathMap) { }
    }

    public static class IncludeableQueryExtensions
    {
        public static IIncludeQuery<TEntity, TNewProperty> Include<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, TPreviousProperty> query,
            Expression<Func<TEntity, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            var id = Guid.NewGuid();
            query.PathMap[id] = query.Visitor.Path;

            return new IncludeQuery<TEntity, TNewProperty>(id, query.PathMap);
        }

        public static IIncludeQuery<TEntity, TNewProperty> ThenInclude<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, TPreviousProperty> query,
            Expression<Func<TPreviousProperty, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            var existingPath = query.PathMap[query.Id];
            query.PathMap[query.Id] = $"{existingPath}.{query.Visitor.Path}";

            return new IncludeQuery<TEntity, TNewProperty>(query.Id, query.PathMap);
        }

        public static IIncludeQuery<TEntity, TNewProperty> ThenInclude<TEntity, TPreviousProperty, TNewProperty>(
            this IIncludeQuery<TEntity, IEnumerable<TPreviousProperty>> query,
            Expression<Func<TPreviousProperty, TNewProperty>> selector)
        {
            query.Visitor.Visit(selector);

            var existingPath = query.PathMap[query.Id];
            query.PathMap[query.Id] = $"{existingPath}.{query.Visitor.Path}";

            return new IncludeQuery<TEntity, TNewProperty>(query.Id, query.PathMap);
        }
    }
}
