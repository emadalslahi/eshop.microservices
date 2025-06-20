using Microsoft.EntityFrameworkCore;

namespace Eshop.Discount.Grpc.Data;

public class DiscountDbContext:DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
    }
    public DbSet<Models.Coupon> Coupons { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Coupon>().HasData(
            new Models.Coupon
            {
                Id = 1,
                ProductName = "IPhone X",
                Description = "IPhone Discount",
                Amount = 150
            },
            new Models.Coupon
            {
                Id = 2,
                ProductName = "Samsung S10",
                Description = "Samsung Discount",
                Amount = 100
            }
        );
    }
}
