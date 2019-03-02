using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetSingleBySpec(ISpecification<T> spec);
        void Update(T entity);
        void Delete(T entity);
        int Count(ISpecification<T> spec);
    }
}
