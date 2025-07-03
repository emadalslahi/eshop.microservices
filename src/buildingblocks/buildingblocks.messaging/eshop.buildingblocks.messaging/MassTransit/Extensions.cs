using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eshop.buildingblocks.messaging.MassTransit;

public static class Extensions
{

    public static IServiceCollection AddMessageBrocker(this IServiceCollection services,
                                                        IConfiguration configuration, 
                                                        Assembly? assembly = null)
    {
        /*
           "MessageBrocker": {
           "Host": "amqp://localhost:5672",
           "UserName": "guest",
           "Password": "guest"
         },
        */

        services.AddMassTransit(cnfg =>{

            // setting Nameing Convention for EndPoints
            cnfg.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                cnfg.AddConsumers(assembly);

            cnfg.UsingRabbitMq((context, confgurator) => {

                confgurator.Host(new Uri(configuration["MessageBrocker:Host"]!), host => {
                    host.Username(configuration["MessageBrocker:UserName"]!);
                    host.Password(configuration["MessageBrocker:Password"]!);
                });
                confgurator.ConfigureEndpoints(context);
            });
        
        });

        // Impement Rabbit MQ masstrasing configurations.
        return services;
    }
}
