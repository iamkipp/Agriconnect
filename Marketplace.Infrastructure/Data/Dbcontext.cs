using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.Entities;

namespace Marketplace.Infrastructure;

public class MarketplaceDbContext : DbContext
{
    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<EscrowLedger> EscrowLedgers => Set<EscrowLedger>();
    public DbSet<PremierBoost> PremierBoosts => Set<PremierBoost>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Product>().HasOne(p => p.Farmer).WithMany().HasForeignKey(p => p.FarmerId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>().HasOne(o => o.Buyer).WithMany().HasForeignKey(o => o.BuyerId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>().HasOne(o => o.Farmer).WithMany().HasForeignKey(o => o.FarmerId).OnDelete(DeleteBehavior.Restrict);

        // Configure relationships
        modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Product).WithMany();
        modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);

        base.OnModelCreating(modelBuilder);
    }
}
