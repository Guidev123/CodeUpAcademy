using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.CreatedAt).IsRequired();

        builder.Property(r => r.Name)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Ignore(x => x.Claims);
    }
}