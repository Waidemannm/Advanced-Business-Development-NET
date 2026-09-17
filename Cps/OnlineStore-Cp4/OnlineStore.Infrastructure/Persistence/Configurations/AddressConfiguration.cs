using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("T_CP1_ADRESS");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired().HasColumnName("ID_ADRESS");
        builder.Property(a => a.CreatedAt).IsRequired().HasColumnName("CREATEDAT");
        builder.Property(a => a.Street).IsRequired().HasMaxLength(150).HasColumnName("STREET");
        builder.Property(a => a.City).IsRequired().HasMaxLength(70).HasColumnName("CITY");
        builder.Property(a => a.State).IsRequired().HasMaxLength(70).HasColumnName("STATE");
        builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(20).HasColumnName("POSTALCODE");
        builder.Property(a => a.Number).IsRequired().HasMaxLength(20).HasColumnName("NUMBER");
        builder.Property(a => a.Country).IsRequired().HasMaxLength(30).HasColumnName("COUNTRY");
    }
}
