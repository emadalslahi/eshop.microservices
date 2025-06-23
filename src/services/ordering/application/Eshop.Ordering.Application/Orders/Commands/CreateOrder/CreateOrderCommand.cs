using eshop.buildingblocks.CQRS;
using Eshop.Ordering.Application.Dtos;
using FluentValidation;

namespace Eshop.Ordering.Application.Orders.Commands.CreateOrder;


public record CreateOrderCommand(OrderDto Order) :ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator :AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();
        RuleFor(x => x.Order.CustomerId).NotEmpty();
        RuleFor(x => x.Order.OrderName).NotEmpty().MaximumLength(100);
        RuleForEach(x => x.Order.OrderItems).NotEmpty();
        //RuleForEach(x => x.Order.OrderItems).SetValidator(new OrderItemDtoValidator());
    }
}