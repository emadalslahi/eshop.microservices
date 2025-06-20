using eshop.buildingblocks.CQRS;
using Eshop.Basket.Api.Data;
using Eshop.Basket.Api.models;
using Eshop.Discount.Grpc.Protos;
using FluentValidation;
using ImTools;

namespace Eshop.Basket.Api.Basket.StoreBasket;


public record StoreBasketCommand(ShopingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(bool IsSuccess, string UserName);


public class StoreBasketCommandValidator :AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("There are No Items in the shoping cart");
        RuleFor(x => x.Cart.UserName).NotEmpty();
        RuleFor(x => x.Cart.TotalPrice).GreaterThan(0);
    }
}

public class StoreBasketCommandHandler(IBasketReposotry reposotry,DiscountService.DiscountServiceClient discountServiceClient) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        await DetectDiscountAmtForCart( request, cancellationToken);

        ShopingCart cart = request.Cart;
        var result = await reposotry.StoreBasket(cart);

        return new StoreBasketResult(true, result.UserName);
    }

    private  async Task DetectDiscountAmtForCart( StoreBasketCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < request.Cart.Items.Count; i++)
        {
            var item = request.Cart.Items[i];
            var discountResponse = await discountServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            if (discountResponse != null && discountResponse.Amount > 0)
            {
                item.Price -= discountResponse.Amount;
            }
        }
    }
}
