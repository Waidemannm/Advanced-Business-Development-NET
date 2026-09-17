using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class RatingProductConfiguration : IEntityTypeConfiguration<RatingProduct>
{
    public void Configure(EntityTypeBuilder<RatingProduct> builder)
    {
        builder.ToTable("T_CP1_RATING_PRODUCT");
        builder.HasKey(r => new { r.IdCostumer, r.IdProduct });
        builder.Property(r => r.IdCostumer).IsRequired().HasColumnName("ID_COSTUMER");
        builder.Property(r => r.IdProduct).IsRequired().HasColumnName("ID_PRODUCT");
        builder.HasOne<Costumer>().WithMany().HasForeignKey(r => r.IdCostumer).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Product>().WithMany().HasForeignKey(r => r.IdProduct).OnDelete(DeleteBehavior.Cascade);
        builder.Property(r => r.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(r => r.Score).IsRequired().HasColumnName("SCORE");
    }
}
