using CodeUp.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Students.Domain.Entities;
using System.Reflection;

namespace Modules.Students.Infrastructure.Persistence;

public sealed class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Address> Address { get; set; } = null!;

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

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}