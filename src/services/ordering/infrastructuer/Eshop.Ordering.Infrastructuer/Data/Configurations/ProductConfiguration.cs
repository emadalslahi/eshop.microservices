using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Ordering.Infrastructuer.Data.Configurations;

public class ProductConfiguration :IEntityTypeConfiguration<Product>
{
    private const int NameMaxLength = 150;
    private const int EmailMaxLength = 255;
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            prodId => prodId.Value,
            dbId => ProductId.Of(dbId)
            );

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(c => c.Price)
            .IsRequired()
            .HasMaxLength(9999);

    }
}
