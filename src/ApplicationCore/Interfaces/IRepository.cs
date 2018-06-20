using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        (IEnumerable<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages) PaginatedListAll(int pageIndex, int itemsPage);
        (IEnumerable<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages) PaginatedList(int pageIndex, int itemsPage, ISpecification<T> spec);
    }
}
