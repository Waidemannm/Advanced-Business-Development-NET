using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence.Configurations;

public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
{
    public void Configure(EntityTypeBuilder<Costumer> builder)
    {
        builder.ToTable("T_CP1_COSTUMER");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("ID_COSTUMER");

        builder.Property(c => c.IdPayment)
            .IsRequired()
            .HasColumnName("ID_PAYMENT");

        builder.HasOne<Payment>()
            .WithMany()
            .HasForeignKey(r => r.IdPayment)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.IdAddress)
            .IsRequired()
            .HasColumnName("ID_ADRESS");

        builder.HasOne<Address>()
            .WithMany()
            .HasForeignKey(r => r.IdAddress)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(c => c.CreatedAt)
            .HasColumnName("CREATEDAT")
            .IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("NAME")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(c =>  c.SetBirthDate)
            .HasColumnName("BIRTHDATE")
            .IsRequired();

        builder.Property(c => c.Gender)
            .HasColumnName("GENDER")
            .HasMaxLength(1)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();
        
    }
}