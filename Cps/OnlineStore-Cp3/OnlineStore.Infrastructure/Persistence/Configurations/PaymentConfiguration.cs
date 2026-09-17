using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("T_CP1_PAYMENT");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired().HasColumnName("ID_PAYMENT");
        builder.Property(p => p.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(p => p.Value).IsRequired().HasPrecision(14, 2).HasColumnName("VALUE");
        builder.Property(p => p.PaymentWay).IsRequired().HasColumnName("PAYMENTWAY");
    }
}
