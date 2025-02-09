using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthenticationDbContext context) : IUserRepository
{
    private readonly AuthenticationDbContext _context = context;

    public async Task<User?> GetByIdAsync(Guid id) 
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email);

    public async Task CreateAsync(User user)
        => await _context.AddAsync(user);

    public void Update(User user)
        => _context.Users.Update(user);

    public async Task CreateUserRoleAsync(UserRole role)
        => await _context.UserRoles.AddAsync(role);

    public async Task<ICollection<string>> GetUserRolesAsync(Guid userId)
        => await _context.UserRoles.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.Role.Name).ToListAsync();

    public async Task DeleteAsync(User user)
        => await _context.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE authentication.Users SET IsActive = 0 WHERE Id = {user.Id}");

    public async Task CreateUserTokenAsync(UserToken userToken)
        => await _context.UserTokens.AddAsync(userToken);

    public async Task<UserToken?> GetTokenByUserIdAsync(Guid userId)
        => await _context.UserTokens.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
