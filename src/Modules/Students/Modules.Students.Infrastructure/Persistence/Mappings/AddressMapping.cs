using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Students.Domain.Entities;

namespace Modules.Students.Infrastructure.Persistence.Mappings;

public sealed class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {

        builder.ToTable("Address");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.IsDeleted).IsRequired();
        builder.Property(a => a.DeletedAt).IsRequired(false);
        builder.Property(a => a.CreatedAt).IsRequired();

        builder.Property(a => a.Street)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.Number)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(a => a.ZipCode)
            .IsRequired()
            .HasColumnType("varchar(20)");

        builder.Property(a => a.AdditionalInfo)
            .HasColumnType("varchar(250)");

        builder.Property(a => a.Neighborhood)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(a => a.City)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(a => a.State)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
