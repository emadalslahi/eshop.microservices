namespace Eshop.Ordering.Application.Orders.Commands.CreateOrder;

internal class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command,
                                          CancellationToken cancellationToken)
    { 
        var ordr = CreateNewOrder(command.Order);
        dbContext.Orders.Add(ordr);
        if(await dbContext.SaveChangesAsync(cancellationToken) > 0) {
         return new CreateOrderResult(ordr.Id.Value);
        }
        throw new InvalidOperationException("Order could not be created.");
    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(
                             frstName: order.ShippingAddress.FirstName,
                             lstName: order.ShippingAddress.LastName,
                             email: order.ShippingAddress.EmailAddress,
                             line: order.ShippingAddress.AddressLine,
                             state: order.ShippingAddress.State,
                             country: order.ShippingAddress.Country,
                             zipCode: order.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(
                             frstName: order.BillingAddress.FirstName,
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
                             method: 1);

        var new_order = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(order.CustomerId),
            orderName: OrderName.Of(order.OrderName),
            shipingAddress: shippingAddress,
            billingAddress: billingAddress,
            status: OrderStatus.Pending,
            payment: payment);

        if (order.OrderItems != null)
        {
            foreach (var item in order.OrderItems)
                new_order.Add(ProductId.Of(item.ProductId),
                           item.Quantity,
                           item.Price
                              );
        }
        return new_order;
    }
}
