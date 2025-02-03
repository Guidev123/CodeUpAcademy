using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthenticationDbContext context) : IUserRepository
{
    private readonly AuthenticationDbContext _context = context;

    public async Task<List<string>> GetRolesAsync(Guid userId)
    {
        var userRole = await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        var roles = await _context.Roles.AsNoTracking().Where(x => x.Id == userRole!.RoleId).ToListAsync();
        return roles.Select(x => x.Name).ToList();
    }

    public async Task<User?> GetByIdAsync(Guid id) 
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<List<UserClaim>> GetUserClaimsAsync(Guid userId) =>
         await _context.UserClaims.AsNoTracking().Where(c => c.UserId == userId).ToListAsync();

    public async Task AddClaimsAsync(List<UserClaim> claims)
    {
        await _context.UserClaims.AddRangeAsync(claims);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveClaimsAsync(Guid userId)
    {
        var claims = _context.UserClaims.AsNoTracking().Where(c => c.UserId == userId);
        _context.UserClaims.RemoveRange(claims);
        await _context.SaveChangesAsync();
    }
}
