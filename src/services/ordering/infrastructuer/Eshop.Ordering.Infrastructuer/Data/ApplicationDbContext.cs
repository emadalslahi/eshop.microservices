using Eshop.Ordering.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eshop.Ordering.Infrastructuer.Data;

public class ApplicationDbContext : DbContext
{
    // Add-Migration InitialCreate -OutputDir Data/Migrations -Project Eshop.Ordering.Infrastructuer -StartupProject Eshop.Ordering.Api
    public ApplicationDbContext(DbContextOptions options)
        :base(options)   { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> Items => Set<OrderItem>();
    //----------------------------------------------

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        // IEntityTypeConfiguration<ApplicationDbContext> configuration 

        // using the configs from seperate classes
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
