using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Domain.Entities;

namespace Modules.Subscriptions.Infrastructure.Persistence;

public sealed class SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : DbContext(options)
{
    public DbSet<Subscription> Subscriptions { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Voucher> Vouchers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
