namespace Modules.Authentication.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync();
}
