namespace Eshop.Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultMaxLength = 255;
    private const int DefaultMinLength = 5;
    public string Value { get; }
    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    { 
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, DefaultMaxLength);
        ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, DefaultMinLength);
        return new OrderName(value);
    }
}

