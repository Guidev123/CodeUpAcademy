using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthenticationDbContext context) : IUserRepository
{
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}