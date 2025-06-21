namespace Eshop.Ordering.Domain.Abstractions;


public interface IAggregate :IEntity
{
    IReadOnlyList<IDomainEvent> Events { get; }
    IDomainEvent[] ClearDomainEvents();
}


public interface IAggregate<T> : IAggregate, IEntity<T>
{

}