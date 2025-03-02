using CodeUp.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Students.Domain.Entities;
using System.Reflection;

namespace Modules.Students.Infrastructure.Persistence;

public sealed class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Address> Address { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        modelBuilder.HasDefaultSchema("students");

        var properties = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties()).Where(x => x.ClrType == typeof(string));
        foreach (var property in properties)
            property.SetColumnType("VARCHAR(160)");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}