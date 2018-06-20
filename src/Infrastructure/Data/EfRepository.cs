using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CatalogContext _dbContext;

        public EfRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return List(spec).FirstOrDefault();
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public (IEnumerable<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages) PaginatedListAll(int pageIndex, int itemsPage)
        {
            var root = _dbContext.Set<T>();

            int totalItems = root.Count();

            var itemsOnPage = root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .AsEnumerable();

            return (itemsOnPage, totalItems, itemsOnPage.Count(), pageIndex, CalcTotalPage(totalItems, itemsPage));
        }
        public async Task<(List<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages)> PaginatedListAllAsync(int pageIndex, int itemsPage)
        {
            var root = _dbContext.Set<T>();

            int totalItems = root.Count();

            var itemsOnPage = await root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToListAsync();

            return (itemsOnPage, totalItems, itemsOnPage.Count(), pageIndex, CalcTotalPage(totalItems, itemsPage));
        }

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        public (IEnumerable<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages) PaginatedList(int pageIndex, int itemsPage, ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            var root = secondaryResult.Where(spec.Criteria);

            int totalItems = root.Count();

            var itemsOnPage = root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .AsEnumerable();

            return (itemsOnPage, totalItems, itemsOnPage.Count(), pageIndex, CalcTotalPage(totalItems, itemsPage));
        }

        public async Task<(List<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages)> PaginatedListAsync(int pageIndex, int itemsPage, ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            var root = secondaryResult.Where(spec.Criteria);

            int totalItems = root.Count();

            var itemsOnPage = await root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToListAsync();

            return (itemsOnPage, totalItems, itemsOnPage.Count(), pageIndex, CalcTotalPage(totalItems, itemsPage));
        }

        protected int CalcTotalPage(int totalItems, int itemsPage)
        {
            return int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString());
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
