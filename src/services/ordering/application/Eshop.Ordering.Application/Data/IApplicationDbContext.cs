using Eshop.Ordering.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Ordering.Application.Data;

public interface IApplicationDbContext
{
   public DbSet<Customer> Customers { get;  }
   public DbSet<Product> Products { get; }
   public DbSet<Order> Orders { get; }
   public DbSet<OrderItem> Items { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
