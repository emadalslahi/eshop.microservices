namespace Eshop.Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // add carter 

        return services;

    }

    public static WebApplication UseApiServices(this WebApplication application) {
        // map carter


        return application;
    }
}
