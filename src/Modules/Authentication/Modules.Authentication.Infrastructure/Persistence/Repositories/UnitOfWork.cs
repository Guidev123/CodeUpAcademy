using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Infrastructure.Persistence.Repositories;

public sealed class UnitOfWork(AuthenticationDbContext context) : IUnitOfWork
{
    private readonly AuthenticationDbContext _context = context;

    public async Task<bool> SaveChangesAsync()
        => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}