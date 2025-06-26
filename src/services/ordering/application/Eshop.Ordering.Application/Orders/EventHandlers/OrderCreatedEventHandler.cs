using Eshop.Ordering.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eshop.Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> _logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, 
                       CancellationToken cancellationToken)
    {
        _logger.LogInformation("OrderCreatedEventHandler: Order with Id {OrderId} created", notification.Order.Id);
        // Here you can add logic to handle the event, such as sending a notification, updating a read model, etc.
        return Task.CompletedTask;
    }
}
