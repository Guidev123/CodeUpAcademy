﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Authentication.Domain.Models;

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

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}