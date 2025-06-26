using Carter;

namespace Eshop.Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // add carter 

        services.AddCarter();
        return services;

    }

    public static WebApplication UseApiServices(this WebApplication application) {
        // map carter

        application.MapCarter();
        return application;
    }
}
