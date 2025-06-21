namespace Eshop.Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }
    private CustomerId(Guid value)=> Value= value;

    public static CustomerId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("CustomerId cannot be Empty.!");

        return new CustomerId(value);
    }
}
