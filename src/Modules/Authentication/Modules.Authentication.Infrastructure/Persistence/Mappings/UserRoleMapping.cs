using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Authentication.Domain.Models;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class UserRoleMapping : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(r => new UserRole(r.RoleId, r.UserId));

        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.Role).WithMany().HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Cascade);
    }
}