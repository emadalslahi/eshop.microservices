using eshop.buildingblocks.messaging.Events;
using Eshop.Ordering.Application.Orders.Commands.CreateOrder;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eshop.Ordering.Application.Orders.EventHandlers.Integration;


public class BasketCheckoutEventHandler(ISender _sender , ILogger<BasketCheckoutEventHandler> _logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = MapToCreateOrderCommand(context.Message);
        await _sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName,
                                        message.LastName,
                                        message.EmailAddress,
                                        message.AddressLine,
                                        message.State,
                                        message.ZipCode,
                                        message.Country);

        var paymntDto = new PaymentDto(message.CardNumber, 
                                       message.CardName, 
                                       message.Expiration, 
                                       message.CVV);

        var orderId = Guid.NewGuid();
        var ordrDto = new OrderDto(Id: orderId,
                                   OrderName:message.UserName,
                                   CustomerId: message.CustomerId,
                                   ShippingAddress: addressDto,
                                   BillingAddress: addressDto,
                                   Payment: paymntDto,
                                   OrderStatus: Ordering.Domain.Enums.OrderStatus.Pending,
                                   OrderItems:[
                                        new OrderItemDto(orderId,new Guid("C11EBB56-19D1-4914-A614-62E448FE4FCC"),4,900),
                                        new OrderItemDto(orderId,new Guid("CD61583A-287F-4505-8681-BBD10B04E530"),3,500),
                                   ]);
        return new CreateOrderCommand(ordrDto);
    }

}
