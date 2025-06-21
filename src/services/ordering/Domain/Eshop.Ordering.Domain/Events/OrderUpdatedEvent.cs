using Eshop.Ordering.Domain.Models;

namespace Eshop.Ordering.Domain.Events;
public record OrderUpdatedEvent(Order Order) :IDomainEvent;