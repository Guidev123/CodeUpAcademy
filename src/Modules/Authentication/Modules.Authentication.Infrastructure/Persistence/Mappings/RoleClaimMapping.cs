using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims");

        builder.HasKey(rc => rc.Id);
        builder.Property(rc => rc.CreatedAt).IsRequired();

        builder.Property(rc => rc.ClaimType)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(rc => rc.ClaimValue)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(500);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(rc => rc.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}