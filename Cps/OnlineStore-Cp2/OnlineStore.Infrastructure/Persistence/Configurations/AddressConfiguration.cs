using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("T_CP1_ADRESS");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("ID_ADDRESS"); 
        
        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasColumnName("CREATEDAT");
 
        builder.Property(r => r.Street)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("STREET");
        
        builder.Property(r => r.City)
            .IsRequired()
            .HasMaxLength(70)
            .HasColumnName("CITY");

        builder.Property(r => r.State)
            .IsRequired()
            .HasMaxLength(70)
            .HasColumnName("STATE");

        builder.Property(r => r.PostalCode)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("POSTALCODE");

        builder.Property(r => r.Number)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("NUMBER");

        builder.Property(r => r.Country)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("COUNTRY");
    }
}