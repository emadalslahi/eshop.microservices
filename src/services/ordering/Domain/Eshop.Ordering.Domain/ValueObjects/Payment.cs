namespace Eshop.Ordering.Domain.ValueObjects;

public record Payment { 
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string ExpiresIn { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() { }

    private Payment(string cardName, string cardNumber,string expiration, string cvv,int method)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        ExpiresIn = expiration;
        CVV = cvv;
        PaymentMethod = method;
    }
    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int method)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);


        return new Payment(cardName, cardNumber, expiration, cvv, method);
    }
}