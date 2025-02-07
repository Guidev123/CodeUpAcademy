using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Enums;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(50);

        builder.HasData(new Role(nameof(SubscriptionTypeEnum.Free), (long)SubscriptionTypeEnum.Free),
                        new Role(nameof(SubscriptionTypeEnum.Premium), (long)SubscriptionTypeEnum.Premium));
    }
}