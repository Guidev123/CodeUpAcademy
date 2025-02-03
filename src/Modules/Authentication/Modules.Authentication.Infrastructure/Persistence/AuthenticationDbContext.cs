using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence;

public sealed class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
}
