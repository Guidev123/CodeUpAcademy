using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class UsersMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.CreatedAt).IsRequired();

        builder.Property(u => u.FirstName)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired();

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);
        });

        builder.OwnsOne(u => u.Phone, phone =>
        {
            phone.Property(p => p.Number)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);
        });

        builder.Property(u => u.BirthDate)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(u => u.PasswordHash)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(500);

        builder.Property(u => u.AccessFailedCount)
            .IsRequired();

        builder.Property(u => u.IsLockedOut)
            .IsRequired();

        builder.Property(u => u.LockoutEnd)
            .IsRequired(false)
            .HasColumnType("datetime2");

        builder.Property(u => u.LastLogin)
            .IsRequired(false)
            .HasColumnType("datetime2");

        builder.Property(u => u.EmailConfirmed)
            .IsRequired();

        builder.Property(u => u.PhoneConfirmed)
            .IsRequired();

        builder.Property(u => u.TwoFactorEnabled)
            .IsRequired();
    }
}
