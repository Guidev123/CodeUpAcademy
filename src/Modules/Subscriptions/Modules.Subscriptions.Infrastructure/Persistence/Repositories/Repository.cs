using CodeUp.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Domain.Repositories;
using System.Linq.Expressions;

namespace Modules.Subscriptions.Infrastructure.Persistence.Repositories;

public sealed class Repository<T>(SubscriptionsDbContext context) : IRepository<T> where T : Entity, IAggregateRoot, new()
{
    private readonly DbSet<T> _entity = context.Set<T>();

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null,
                                      int? pageNumber = null, int? pageSize = null)
    {
        var query = _entity.AsQueryable();

        if (expression is not null)
            query = query.Where(expression);

        if (include is not null)
            query = include(query);

        if (pageNumber is not null && pageNumber.HasValue)
            query = query.Skip(pageNumber.Value);

        if (pageSize is not null && pageSize.HasValue)
            query = query.Take(pageSize.Value);

        return await query.ToListAsync().ConfigureAwait(false);
    }

    public async Task<T?> GetById(Guid id)
        => await _entity.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression)
        => await _entity.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(expression).ConfigureAwait(false);

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        => await _entity.AsNoTrackingWithIdentityResolution().CountAsync(expression).ConfigureAwait(false);

    public async Task CreateAsync(T entity)
        => await _entity.AddAsync(entity);

    public void Update(T entity)
        => _entity.Update(entity);
}