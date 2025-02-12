using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Students.Domain.Entities;

namespace Modules.Students.Infrastructure.Persistence.Mappings;

public sealed class StudentMapping : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.IsDeleted).IsRequired();
        builder.Property(a => a.DeletedAt).IsRequired(false);
        builder.Property(a => a.CreatedAt).IsRequired();

        builder.OwnsOne(x => x.Email, opt =>
        {
            opt.Property(x => x.Address).HasColumnType("VARCHAR(80)").IsRequired().HasColumnName("Email");
        });

        builder.OwnsOne(x => x.Phone, opt =>
        {
            opt.Property(x => x.Number).HasColumnType("VARCHAR(40)").IsRequired().HasColumnName("Phone");
        });

        builder.OwnsOne(x => x.Document, opt =>
        {
            opt.Property(x => x.Number).HasColumnType("VARCHAR(40)").IsRequired().HasColumnName("Document");
        });

        builder.Property(x => x.ProfilePicture).HasColumnType("VARCHAR(255)");

        builder.HasOne(x => x.Address).WithOne(x => x.Student).HasForeignKey<Address>(x => x.StudentId);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
