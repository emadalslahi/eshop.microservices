using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Eshop.Discount.Grpc.Data;

public static class Extensions
{
    public static  IApplicationBuilder UseMigration(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
        if (dbContext.Database.IsRelational())
        {
             dbContext.Database.MigrateAsync();
        }
        else
        {
             dbContext.Database.EnsureCreatedAsync();
        }

        return app;
    }
}
