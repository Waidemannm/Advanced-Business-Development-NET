using Microsoft.EntityFrameworkCore;
using OnlineSore.Domain.Entities;

namespace OnlineInfrastructure.Persistence;

public class OnlineStoreContext : DbContext
{
    public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
    {
    }
    
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Costumer> Costumers { get; set; }

    public DbSet<Payment> Payments { get; set; }
    
    public DbSet<Product>  Products{ get; set; }
    
    public DbSet<RatingProduct> RatingProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineStoreContext).Assembly);
    }
}