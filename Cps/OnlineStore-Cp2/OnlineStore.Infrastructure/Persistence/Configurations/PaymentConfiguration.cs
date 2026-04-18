using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using OnlineSore.Domain.Entities;                

namespace OnlineInfrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("T_CP1_PAYMENT");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("ID_PAYMENT"); 
        
        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasColumnName("CREATEDAT");
        
        builder.Property(r => r.PaymentWay)
            .IsRequired()
            .HasMaxLength(1)
            .HasColumnName("PAYMENTWAY");
        
        builder.Property(r => r.Value)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasColumnName("VALUE");
    }
}