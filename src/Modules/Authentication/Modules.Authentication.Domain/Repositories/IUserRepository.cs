using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Domain.Repositories;

public interface IUserRepository : IDisposable
{
    Task<List<string>> GetRolesAsync(Guid userId);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<List<UserClaim>> GetUserClaimsAsync(Guid userId);
    Task AddClaimsAsync(List<UserClaim> claims);
    Task RemoveClaimsAsync(Guid userId);
}
