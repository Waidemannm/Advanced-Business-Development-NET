using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder){
        builder.ToTable("T_CP1_PRODUCT");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("ID_PRODUCT"); 
        
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.IdCategory)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasColumnName("CREATEDAT");

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(300)
            .HasColumnName("NAME");

        builder.HasIndex(r => r.Name).IsUnique();    

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("DESCRIPTION");

        builder.Property(r => r.Price)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasColumnName("PRICE");

        builder.Property(r => r.Stock)
            .IsRequired()
            .HasMaxLength(5)
            .HasColumnName("STOCK");
    }
}