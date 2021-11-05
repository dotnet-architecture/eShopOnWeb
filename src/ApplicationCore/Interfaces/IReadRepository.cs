using Ardalis.Specification;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
