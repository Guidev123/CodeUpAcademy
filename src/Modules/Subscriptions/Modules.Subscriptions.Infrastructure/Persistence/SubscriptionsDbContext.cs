using CodeUp.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Domain.Entities;
using System.Reflection;

namespace Modules.Subscriptions.Infrastructure.Persistence;

public sealed class SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : DbContext(options)
{
    public DbSet<Subscription> Subscriptions { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Voucher> Vouchers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        var properties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(p => p.GetProperties())
            .Where(p => p.ClrType == typeof(string));

        foreach (var property in properties)
            property.SetColumnType("varchar(160)");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}