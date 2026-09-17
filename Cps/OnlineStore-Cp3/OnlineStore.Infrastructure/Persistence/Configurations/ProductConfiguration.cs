using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("T_CP1_PRODUCT");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired().HasColumnName("ID_PRODUCT");
        builder.HasOne<Category>().WithMany().HasForeignKey(p => p.IdCategory).OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(p => p.Name).IsRequired().HasMaxLength(300).HasColumnName("NAME");
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500).HasColumnName("DESCRIPTION");
        builder.Property(p => p.Price).IsRequired().HasPrecision(14, 2).HasColumnName("PRICE");
        builder.Property(p => p.Stock).IsRequired().HasColumnName("STOCK");
    }
}
