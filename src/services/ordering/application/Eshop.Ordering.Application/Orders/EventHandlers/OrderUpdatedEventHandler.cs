using Eshop.Ordering.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eshop.Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> _logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification,
                   CancellationToken cancellationToken)
    {
        _logger.LogInformation("OrderUpdatedEventHandler: Order with Id {OrderId} Updated ", notification.Order.Id);
        // Here you can add logic to handle the event, such as sending a notification, updating a read model, etc.
        return Task.CompletedTask;
    }
}
