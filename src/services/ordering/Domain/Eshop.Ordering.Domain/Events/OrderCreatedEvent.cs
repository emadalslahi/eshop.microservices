using Eshop.Ordering.Domain.Models;

namespace Eshop.Ordering.Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;
