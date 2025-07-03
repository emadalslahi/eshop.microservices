using eshop.buildingblocks.CQRS;
using eshop.buildingblocks.messaging.Events;
using Eshop.Basket.Api.Data;
using Eshop.Basket.Api.Dtos;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Eshop.Basket.Api.Basket.CheckoutBasket;


public record BasketCheckoutResult(bool IsSuccess);
public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
        :ICommand<BasketCheckoutResult>;


public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("Basket Checkout Dto Should not Be Null !");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty();
    }

}

public class CheckoutBasketHandler(IBasketReposotry _reposotry ,IPublishEndpoint _publish) 
    : ICommandHandler<CheckoutBasketCommand, BasketCheckoutResult>
{

    public async Task<BasketCheckoutResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // set totalprice on basketchecout event message
        // send basket checkout event to rabbitmq usgin masstransit
        //delete the basket

        var basket = await _reposotry.GetBasket(command.BasketCheckoutDto.UserName,cancellationToken);
        if (basket == null) { 
         return new BasketCheckoutResult(false);
        }

        var eventMsg = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        if (eventMsg == null)
        {
            return new BasketCheckoutResult(false);
        }
        
        eventMsg.TotalPrice = basket.TotalPrice;

        await _publish.Publish(eventMsg,cancellationToken);

        await _reposotry.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        return new BasketCheckoutResult(true);
    }
}
