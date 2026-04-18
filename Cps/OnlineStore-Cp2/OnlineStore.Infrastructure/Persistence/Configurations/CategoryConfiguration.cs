using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("T_CP1_CATEGORY");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("ID_CATEGORY");
        
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
            .HasMaxLength(150)
            .HasColumnName("DESCRIPTION");
    }
}