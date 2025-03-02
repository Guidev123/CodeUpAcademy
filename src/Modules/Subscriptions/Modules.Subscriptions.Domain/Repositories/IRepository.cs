using CodeUp.SharedKernel.DomainObjects;
using System.Linq.Expressions;

namespace Modules.Subscriptions.Domain.Repositories;

public interface IRepository<T> where T : Entity, IAggregateRoot
{
    Task CreateAsync(T entity);

    Task<T?> GetById(Guid id);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null,
                               int? pageNumber = null, int? pageSize = null);

    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression);

    Task<int> CountAsync(Expression<Func<T, bool>> expression);

    void Update(T entity);
}