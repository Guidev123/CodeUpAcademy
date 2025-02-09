using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class UserTokenMapping : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("UserTokens");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.ExpiresAt).IsRequired();
    }
}
