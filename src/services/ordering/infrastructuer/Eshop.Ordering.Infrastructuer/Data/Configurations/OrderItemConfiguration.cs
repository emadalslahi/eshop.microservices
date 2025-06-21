using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Ordering.Infrastructuer.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x=> x.Id);

        builder.Property(x => x.Id).HasConversion(
                                                orderItemId => orderItemId.Value,
                                                dbId=>OrderItemId.Of(dbId)
                                                );

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(x => x.Price).IsRequired();
        builder.Property(x=>x.Quantity).IsRequired();

    }
}
