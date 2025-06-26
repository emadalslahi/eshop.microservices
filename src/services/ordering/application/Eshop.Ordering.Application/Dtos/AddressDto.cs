namespace Eshop.Ordering.Application.Dtos;

public record AddressDto(
    string? FirstName,
    string? LastName,
    string? EmailAddress,
    string? AddressLine,
    string? State,
    string? ZipCode,
    string? Country
    );
