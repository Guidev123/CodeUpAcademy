using Modules.Authentication.Domain.Models;

namespace Modules.Authentication.Domain.Repositories;

public interface IUserRepository : IDisposable
{
    Task CreateAsync(User user);
    Task CreateUserRoleAsync(UserRole role);
    Task CreateUserTokenAsync(UserToken userToken);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<UserToken?> GetTokenByUserIdAsync(Guid userId);
    Task<ICollection<string>> GetUserRolesAsync(Guid userId);
    void Update(User user);
    Task DeleteAsync(User user);
}
