using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class UserClaimMapping : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("UserClaims");

        builder.HasKey(uc => uc.Id);
        builder.Property(uc => uc.CreatedAt).IsRequired();

        builder.Property(uc => uc.ClaimType)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(uc => uc.ClaimValue)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(500);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}