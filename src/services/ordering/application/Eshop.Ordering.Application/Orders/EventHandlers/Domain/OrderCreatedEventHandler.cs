using Eshop.Ordering.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
namespace Eshop.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint _publisher,
                                      IFeatureManager _featuerManager,
                                      ILogger<OrderCreatedEventHandler> _logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent,
                       CancellationToken cancellationToken)
    {

        if (domainEvent.Order is null)
        {
            _logger.LogError("Order is Null");
            return;
        }

        _logger.LogInformation("OrderCreatedEventHandler: Order with Id {OrderId} created",
                         domainEvent.Order.Id);

        // only Send notification if Featuer is Enable [ not seeding data time ]
        if (await _featuerManager.IsEnabledAsync("OrderFullfilment"))
        {
            OrderDto orderCreatedIntegrationEvnt = domainEvent.Order.ToDto();

            await _publisher.Publish(orderCreatedIntegrationEvnt, cancellationToken);

        }
        else
        {
            _logger.LogInformation("OrderCreatedEventHandler: Order with Id {OrderId} Was Not Allowed To Publish Becouse of Featuer Not Enabled",
                         domainEvent.Order.Id);
        }

    }
}