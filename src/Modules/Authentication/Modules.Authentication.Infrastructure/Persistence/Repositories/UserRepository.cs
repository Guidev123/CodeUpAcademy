using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AuthenticationDbContext context) : IUserRepository
{
    private readonly AuthenticationDbContext _context = context;

    public async Task<List<string>> GetRolesAsync(Guid userId)
    {
        var user = await _context.Users.AsNoTrackingWithIdentityResolution()
                                       .Include(u => u.Claims)
                                       .ThenInclude(c => c.Role)
                                       .FirstOrDefaultAsync(u => u.Id == userId);

        return user?.Claims.Select(c => c.Role.Name).ToList() ?? [];
    }

    public async Task<User?> GetByIdAsync(Guid id) 
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email);


    public async Task<List<UserClaim>> GetUserClaimsAsync(Guid userId) =>
         await _context.UserClaims.AsNoTracking().Where(c => c.UserId == userId).ToListAsync();

    public async Task AddClaimsAsync(List<UserClaim> claims)
        => await _context.UserClaims.AddRangeAsync(claims);

    public async Task RemoveClaimsAsync(Guid userId)
    {
        var claims = _context.UserClaims.Where(c => c.UserId == userId);
        _context.UserClaims.RemoveRange(_context.UserClaims.Where(c => c.UserId == userId));
        await _context.SaveChangesAsync();
    }

    public async Task CreateAsync(User user)
        => await _context.AddAsync(user);

    public async Task AddClaimAsync(UserClaim claims) 
        => await _context.UserClaims.AddAsync(claims);

    public void Update(User user)
        => _context.Users.Update(user);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
