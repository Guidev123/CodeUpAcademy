﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Authentication.Domain.Entities;

namespace Modules.Authentication.Infrastructure.Persistence.Mappings;

public sealed class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.CreatedAt).IsRequired();

        builder.Property(rt => rt.UserEmail)
            .IsRequired().HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(rt => rt.Token)
            .IsRequired();

        builder.Property(rt => rt.ExpirationDate)
            .IsRequired().HasColumnType("VARCHAR")
            .HasColumnType("datetime2");
    }
}