using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence.Configurations;

public class RatingProductConfiguration : IEntityTypeConfiguration<RatingProduct>
{
    public void Configure(EntityTypeBuilder<RatingProduct> builder)
    {
        builder.ToTable("T_CP1_RATING_PRODUCT");
        
        builder.HasKey(r => new {r.IdCostumer, r.IdProduct});

        builder.Property(r => r.IdCostumer)
            .IsRequired()
            .HasColumnName("ID_COSTUMER");

        builder.Property(r => r.IdProduct)
            .IsRequired()
            .HasColumnName("ID_PRODUCT");      
            
        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasColumnName("CREATEDAT");
        
        builder.Property(r => r.Score)
            .IsRequired()
            .HasMaxLength(1)
            .HasColumnName("SCORE");
        
        builder.HasOne<Costumer>()
            .WithMany()
            .HasForeignKey(r => r.IdCostumer)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(r => r.IdProduct)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(u => new {u.IdProduct, u.IdCostumer})
            .IsUnique();
    }
}