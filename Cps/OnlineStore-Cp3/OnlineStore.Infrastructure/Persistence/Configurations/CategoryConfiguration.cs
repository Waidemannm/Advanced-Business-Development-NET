using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("T_CP1_CATEGORY");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("ID_CATEGORY");
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(300).HasColumnName("NAME");
        builder.HasIndex(c => c.Name).IsUnique();
        builder.Property(c => c.Description).IsRequired().HasMaxLength(150).HasColumnName("DESCRIPTION");
    }
}
