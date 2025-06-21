using Eshop.Ordering.Domain.Enums;
using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Ordering.Infrastructuer.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
                                    ordrId => ordrId.Value,
                                    dbId =>OrderId.Of(dbId)
                                                );

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(ordr => ordr.CustomerId)
            .IsRequired();

        builder.HasMany(ordr => ordr.OrderItems)
               .WithOne()
               .HasForeignKey(oi => oi.OrderId);


        builder.ComplexProperty(e=>e.OrderName , nameBldr => {
            nameBldr.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });


        builder.ComplexProperty(
            o=>o.ShippingAddress,addrsBldr=>
            {
                addrsBldr.Property(a=>a.FirstName)
                .HasMaxLength(100)
                .IsRequired();
                addrsBldr.Property(a => a.LastName)
                 .HasMaxLength(100)
                .IsRequired();
                addrsBldr.Property(a => a.AddressLine)
                                .HasMaxLength(100)
                               .IsRequired();
                addrsBldr.Property(a => a.Country)
                .HasMaxLength(50) ;
                addrsBldr.Property(a => a.EmailAddress)
                .HasMaxLength(100);
                addrsBldr.Property(a => a.State)
                                .HasMaxLength(100);
                addrsBldr.Property(a => a.ZipCode)
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.BillingAddress, addrsBldr =>
            {
                addrsBldr.Property(a => a.FirstName)
                .HasMaxLength(100)
                .IsRequired();
                addrsBldr.Property(a => a.LastName)
                 .HasMaxLength(100)
                .IsRequired();
                addrsBldr.Property(a => a.AddressLine)
                                .HasMaxLength(100)
                               .IsRequired();
                addrsBldr.Property(a => a.Country)
                .HasMaxLength(50);
                addrsBldr.Property(a => a.EmailAddress)
                .HasMaxLength(100);
                addrsBldr.Property(a => a.State)
                                .HasMaxLength(100);
                addrsBldr.Property(a => a.ZipCode)
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, pymntBldr =>
            {
                pymntBldr.Property(a => a.CardName)
                .HasMaxLength(50)
                .IsRequired();
                pymntBldr.Property(a => a.CardNumber)
                 .HasMaxLength(24)
                .IsRequired();
                pymntBldr.Property(a => a.ExpiresIn)
                                .HasMaxLength(10);

                pymntBldr.Property(a => a.CVV)
                .HasMaxLength(3);

                pymntBldr.Property(p=>p.PaymentMethod);
            });

        builder.Property(e=>e.Status)
               .HasDefaultValue(OrderStatus.Drafted)
               .HasConversion(
                             s        => s.ToString(),
                             dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbstatus)
                             );
        builder.Property(p => p.TotalPrice);

    }
}
