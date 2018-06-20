using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<(List<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages)> PaginatedListAllAsync(int pageIndex, int itemsPage);
        Task<(List<T> Items, int TotalItems, int ItemsPerPage, int ActualPage, int TotalPages)> PaginatedListAsync(int pageIndex, int itemsPage, ISpecification<T> spec);
    }
}
