using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
{
    public void Configure(EntityTypeBuilder<Costumer> builder)
    {
        builder.ToTable("T_CP1_COSTUMER");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("ID_COSTUMER");
        builder.Property(c => c.IdPayment).IsRequired().HasColumnName("ID_PAYMENT");
        builder.HasOne<Payment>().WithMany().HasForeignKey(c => c.IdPayment).OnDelete(DeleteBehavior.Cascade);
        builder.Property(c => c.IdAddress).IsRequired().HasColumnName("ID_ADRESS");
        builder.HasOne<Address>().WithMany().HasForeignKey(c => c.IdAddress).OnDelete(DeleteBehavior.Cascade);
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(300).HasColumnName("NAME");
        builder.Property(c => c.BirthDate).IsRequired().HasColumnName("BIRTHDATE");
        builder.Property(c => c.Gender).IsRequired().HasColumnName("GENDER");
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100).HasColumnName("EMAIL");
        builder.HasIndex(c => c.Email).IsUnique();
    }
}
