using Ardalis.Specification;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
