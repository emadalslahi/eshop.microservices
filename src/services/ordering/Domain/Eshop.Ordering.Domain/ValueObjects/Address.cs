namespace Eshop.Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get;  } = default!;
    public string LastName { get;  } = default!;
    public string AddressLine { get; } = default!;
    public string? EmailAddress { get;   } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address() { }

    private Address(string frstName,
                    string lstName, 
                    string line,
                    string email,
                    string country, 
                    string state,
                    string zipCode)
    {
        FirstName = frstName;
        LastName = lstName;
        EmailAddress = email;
        Country = country;
        State = state;
        ZipCode = zipCode;
        AddressLine = line;
    }
    public static Address Of(string frstName,
                    string lstName,
                    string email,
                    string line,
                    string country,
                    string state,
                    string zipCode)
    { 
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(line, nameof(line));
        
        return new Address(frstName,
                           lstName, 
                           email, 
                           line, 
                           country,
                           state,
                           zipCode);
    }
}
