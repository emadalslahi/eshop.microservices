namespace Eshop.Ordering.Application.Extensions;

public static class OrderExtensions
{
  public  static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        // Map the orders to OrderDto
        return orders.Select(order => new OrderDto
            (
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                OrderStatus: order.Status,
                ShippingAddress: new AddressDto
                (
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress,
                    AddressLine: order.ShippingAddress.AddressLine,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode,
                    Country: order.ShippingAddress.Country
                ),
                BillingAddress: new AddressDto
                (
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    EmailAddress: order.BillingAddress.EmailAddress,
                    AddressLine: order.BillingAddress.AddressLine,
                    State: order.BillingAddress.State,
                    ZipCode: order.BillingAddress.ZipCode,
                    Country: order.BillingAddress.Country
                ),
                Payment: new PaymentDto
                (
                    CardName: order.Payment.CardName,
                    CardNumber: order.Payment.CardNumber,
                    ExpiresIn: order.Payment.ExpiresIn,
                    Cvv: order.Payment.CVV
                ),
                OrderItems: order.OrderItems.Select(
                    item => new OrderItemDto
                            (
                                OrderId: item.OrderId.Value,
                                ProductId: item.ProductId.Value,
                                Quantity: item.Quantity,
                                Price: item.Price
                            )).ToList()
            ));
    }
}
