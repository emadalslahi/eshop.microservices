using FluentValidation;

namespace Eshop.Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid id) :ICommand<DeleteOrderResult>;
public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Order Id cannot be empty.");
    }
}

