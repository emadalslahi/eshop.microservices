namespace Eshop.Ordering.Application.Dtos;

public record PaymentDto(
    string CardNumber,
    string CardName,
    string ExpiresIn,
    string Cvv
    );
