using Microsoft.EntityFrameworkCore;
using POS.Persistence.Model;

namespace POS.Persistence.Data;

public class PizzaOrderDbContext(DbContextOptions<PizzaOrderDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasOne(d => d.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(fk => fk.CustomerId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");


            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("double precision");
        });
    }
}