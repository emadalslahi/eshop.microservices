using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;

namespace Eshop.Ordering.Infrastructuer.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scop = app.Services.CreateScope();
        var context = scop.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomersAsync(context);
        await SeedProductsAsync(context);
        await SeedOrdersAndItemsAsync(context);
    }
    private static async Task SeedCustomersAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }



    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedOrdersAndItemsAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }

}
public static class InitialData
{

    public static IEnumerable<Customer> Customers =>
       new List<Customer>
       {
           Customer.Create(CustomerId.Of(new Guid("DFC89CAC-2D9B-4242-B43E-50B437E6BB75")),"EmadSlahi","eng.emadslahi@omdh.com"),
           Customer.Create(CustomerId.Of(new Guid("03E449BD-AE09-4138-BA9B-026F8F0BF6EF")),"Mohammed Slahi","eng.moho@omdh.com"),
           Customer.Create(CustomerId.Of(new Guid("037A5200-9245-43F3-8363-7A5905F8B261")),"Slah Slahi","eng.salh@omdh.com"),
           Customer.Create(CustomerId.Of(new Guid("B61A7E1E-9AD3-48FE-BF3A-B599557C60FD")),"Azooz Farhan","eng.azzoz@omdh.com"),
       };

    public static IEnumerable<Product> Products => new List<Product>
    {
    Product.Create(ProductId.Of(new Guid("CD61583A-287F-4505-8681-BBD10B04E530")),"IPhone X",544.23m),
    Product.Create(ProductId.Of(new Guid("A8A914CB-55C6-42E1-A5FE-4E58C58272BE")),"Samsung G24",784.23m),
    Product.Create(ProductId.Of(new Guid("C11EBB56-19D1-4914-A614-62E448FE4FCC")),"Dell Inpirsion",4234.23m),
    Product.Create(ProductId.Of(new Guid("46A87EB6-FCC0-45AD-8979-7688E6B0EA90")),"Sozuki Vitara 2011",5000)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            

            var Order01 = Order.Create(OrderId.Of(Guid.NewGuid()),
                            customerId: CustomerId.Of(new Guid("b61a7e1e-9ad3-48fe-bf3a-b599557c60fd")),
                            OrderName.Of("Order For Emad Slahi"),
                            shipingAddress: Address.Of("Emad", "Slahi", "emad@slahi.com", "Sanaa Aser Street", "Yemen", "Sanaa", "967"),
                            billingAddress: Address.Of("Emad", "Slahi", "emad@slahi.com", "Sanaa Aser Street", "Yemen", "Sanaa", "967"),
                            Domain.Enums.OrderStatus.Pending,
                            Payment.Of("OmdhCardName", "77150", DateTime.Now.ToShortDateString(), "199", 1));

            //var Order02 = Order.Create(OrderId.Of(new Guid("6EED5427-161F-417D-8347-B9D915F9ADA3")),
            //            Customers.ElementAt(1).Id,
            //            OrderName.Of("Order For Salah Slahi"),
            //            Address.Of("Emad", "Slahi", "salah@slahi.com", "Taiz Aser Street", "Yemen", "Sanaa", "967"),
            //            Address.Of("Emad", "Slahi", "salah@slahi.com", "Taiz Aser Street", "Yemen", "Sanaa", "967"),
            //            Domain.Enums.OrderStatus.Pending,
            //            Payment.Of("SlhCardName", "77150", DateTime.Now.ToShortDateString(), "199", 1));
            //var Order03 = Order.Create(OrderId.Of(new Guid("D97CE6E9-3621-41E0-9007-25ABE7936F4F")),
            //             Customers.ElementAt(2).Id,
            //             OrderName.Of("Order For Mohammed Slahi"),
            //             Address.Of("Emad", "Slahi", "moho@slahi.com", "Sanaa Aser Street", "Yemen", "Sanaa", "967"),
            //             Address.Of("Emad", "Slahi", "mooho@slahi.com", "Sanaa Aser Street", "Yemen", "Sanaa", "967"),
            //             Domain.Enums.OrderStatus.Pending,
            //             Payment.Of("MohoCardName", "77150", DateTime.Now.ToShortDateString(), "199", 1));

            //Order01.Add(Products.ElementAt(0).Id, 4, 4000);
            //Order02.Add(Products.ElementAt(2).Id, 4, 5000);

            //Order03.Add(Products.ElementAt(1).Id, 4, 4000);

            return new List<Order> { Order01 };
        }
    }
}
