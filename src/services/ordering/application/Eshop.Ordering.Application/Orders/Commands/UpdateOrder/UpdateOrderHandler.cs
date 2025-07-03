namespace Eshop.Ordering.Application.Orders.Commands.UpdateOrder;

internal class UpateOrderCommandHandler(IApplicationDbContext dbContext) :
    ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command,
                                          CancellationToken cancellationToken)
    {

        var orderId = OrderId.Of(command.Order.Id);
        var ordr = await dbContext.Orders.FindAsync([orderId] ,cancellationToken);
        if (ordr == null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrder(ordr, command.Order);

        if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
        {
            return new UpdateOrderResult(true);
        }
        return new UpdateOrderResult(false);

    }

    private void UpdateOrder(Order ordr, OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
                             frstName: orderDto.ShippingAddress.FirstName,
                             lstName: orderDto.ShippingAddress.LastName,
                             email: orderDto.ShippingAddress.EmailAddress,
                             line: orderDto.ShippingAddress.AddressLine,
                             state: orderDto.ShippingAddress.State,
                             country: orderDto.ShippingAddress.Country,
                             zipCode: orderDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(
                             frstName: orderDto.BillingAddress.FirstName,
                             lstName: orderDto.BillingAddress.LastName,
                             email: orderDto.BillingAddress.EmailAddress,
                             line: orderDto.BillingAddress.AddressLine,
                             state: orderDto.BillingAddress.State,
                             country: orderDto.BillingAddress.Country,
                             zipCode: orderDto.BillingAddress.ZipCode);
        var payment = Payment.Of(
                             cardNumber: orderDto.Payment.CardNumber,
                             cardName: orderDto.Payment.CardName,
                             expiration: orderDto.Payment.ExpiresIn,
                             cvv: orderDto.Payment.Cvv,
                             method: 1);
        ordr.Update(
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shipingAddress: shippingAddress,
            billingAddress: billingAddress,
            status: OrderStatus.Pending,
            payment: payment);   
    }
}
