using CodeUp.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence;

public sealed class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        var properties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(p => p.GetProperties())
            .Where(p => p.ClrType == typeof(string)
            && p.GetColumnType() == null);

        foreach (var item in properties)
        {
            item.SetColumnType("VARCHAR(160)");
            item.SetIsUnicode(false);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
    }
}