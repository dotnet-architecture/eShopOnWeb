using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{

    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
