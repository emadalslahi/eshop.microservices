namespace Eshop.Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("OrderItemId cannot be Empty.!");

        return new OrderItemId(value);
    }
}
