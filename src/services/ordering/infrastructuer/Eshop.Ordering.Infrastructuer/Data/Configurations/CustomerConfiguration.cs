using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Ordering.Infrastructuer.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    private const int NameMaxLength = 150;
    private const int EmailMaxLength = 255;
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            customerid=>customerid.Value,
            dbId =>CustomerId.Of(dbId)
            );

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(c => c.Email)
            .IsRequired()          
            .HasMaxLength(EmailMaxLength);

        builder.HasIndex(c => c.Email).IsUnique();

    }
}
