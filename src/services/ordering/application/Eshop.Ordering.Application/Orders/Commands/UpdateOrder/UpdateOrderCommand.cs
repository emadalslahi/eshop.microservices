using FluentValidation;

namespace Eshop.Ordering.Application.Orders.Commands.UpdateOrder;


public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();
        RuleFor(x => x.Order.Id).NotEmpty();
        RuleFor(x => x.Order.CustomerId).NotEmpty();
        RuleFor(x => x.Order.OrderName).NotEmpty().MaximumLength(100);
        RuleForEach(x => x.Order.OrderItems).NotEmpty();
        //RuleForEach(x => x.Order.OrderItems).SetValidator(new OrderItemDtoValidator());
    }
}