

namespace Eshop.Ordering.Infrastructuer;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        //--------- Adding Services to container    
        services.AddDbContext<ApplicationDbContext>(
                                options => options.UseSqlServer(connectionString));
        return services;
    }
}
