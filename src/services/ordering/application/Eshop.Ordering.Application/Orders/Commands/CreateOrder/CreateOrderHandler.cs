using eshop.buildingblocks.CQRS;
using Eshop.Ordering.Application.Data;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Domain.Enums;
using Eshop.Ordering.Domain.Models;
using Eshop.Ordering.Domain.ValueObjects;
using System.Reflection.Emit;

namespace Eshop.Ordering.Application.Orders.Commands.CreateOrder;

internal class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, 
                                          CancellationToken cancellationToken)
    {
        // create order entity from command object
        // save to databes
        // return rslt


        var ordr = CreateNewOrder(command.Order);
        dbContext.Orders.Add(ordr);
        await dbContext.SaveChangesAsync(cancellationToken);

        throw new NotImplementedException();
    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(
                             frstName:    order.ShippingAddress.FirestName,
                             lstName :   order.ShippingAddress.LastName,
                             email  : order.ShippingAddress.EmailAddress,
                             line    :order.ShippingAddress.AddressLine,
                             state :   order.ShippingAddress.State,
                             country  : order.ShippingAddress.Country,
                             zipCode:  order.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(
                             frstName: order.BillingAddress.FirestName,
                             lstName: order.BillingAddress.LastName,
                             email: order.BillingAddress.EmailAddress,
                             line: order.BillingAddress.AddressLine,
                             state: order.BillingAddress.State,
                             country: order.BillingAddress.Country,
                             zipCode: order.BillingAddress.ZipCode);
        var payment = Payment.Of(
                             cardNumber: order.Payment.CardNumber,
                             cardName: order.Payment.CardName,
                             expiration: order.Payment.ExpiresIn,
                             cvv: order.Payment.Cvv,
                             method:1);

        var new_order = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(order.CustomerId),
            orderName: OrderName.Of(order.OrderName),
            shipingAddress: shippingAddress,
            billingAddress: billingAddress,
            status: OrderStatus.Pending,
            payment: payment);

        foreach (var item in order.OrderItems)
            new_order.Add(ProductId.Of(item.ProductId),
                       item.Quantity,
                       item.Price
                          );
        return new_order;
    }
}
