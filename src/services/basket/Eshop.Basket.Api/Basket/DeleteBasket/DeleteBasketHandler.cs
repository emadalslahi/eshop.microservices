using eshop.buildingblocks.CQRS;
using Eshop.Basket.Api.Basket.StoreBasket;
using Eshop.Basket.Api.Data;
using Eshop.Basket.Api.models;
using FluentValidation;
using ImTools;

namespace Eshop.Basket.Api.Basket.DeleteBasket;


public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}

public class DeleteBasketCommandHandler(IBasketReposotry reposotry) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await reposotry.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}
