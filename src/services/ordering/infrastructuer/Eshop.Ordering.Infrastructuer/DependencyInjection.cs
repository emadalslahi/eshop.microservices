

using Eshop.Ordering.Application.Data;
using Eshop.Ordering.Infrastructuer.Data.Intercepters;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eshop.Ordering.Infrastructuer;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        //--------- Adding Services to container    

        //--- add scope to bcz its require medator di in dispatch 
        //--- this way asp .net will provide the new instance of the interceptor
        //---- so we use sp.getservices<ISaveChangesInterceptor>() to get all the interceptors
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityIntercepter>();
        services.AddScoped<ISaveChangesInterceptor, DispachDomainEventInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
                                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors()
                                .UseSqlServer(connectionString,
                                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());

        });

        services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
        //  
        return services;
    }
}
