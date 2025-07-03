
using Eshop.Ordering.Domain.Models;

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
                    EmailAddress: order.ShippingAddress.EmailAddress!,
                    AddressLine: order.ShippingAddress.AddressLine,
                    State: order.ShippingAddress.State,
                    ZipCode: order.ShippingAddress.ZipCode,
                    Country: order.ShippingAddress.Country
                ),
                BillingAddress: new AddressDto
                (
                    FirstName: order.BillingAddress.FirstName,
                    LastName: order.BillingAddress.LastName,
                    EmailAddress: order.BillingAddress.EmailAddress!,
                    AddressLine: order.BillingAddress.AddressLine,
                    State: order.BillingAddress.State,
                    ZipCode: order.BillingAddress.ZipCode,
                    Country: order.BillingAddress.Country
                ),
                Payment: new PaymentDto
                (
                    CardName: order.Payment.CardName!,
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


    public static OrderDto ToDto(this Order ordr)
    {
        return new OrderDto(
            Id: ordr.Id.Value,
            CustomerId: ordr.CustomerId.Value,
            OrderName: ordr.OrderName.Value,
            ShippingAddress: ordr.ShippingAddress.ToDto(),
            BillingAddress: ordr.BillingAddress.ToDto(),
            Payment: ordr.Payment.ToDto(),
            OrderStatus: ordr.Status,
            OrderItems: ordr.OrderItems.ToDto().ToList()
            //ordr.OrderItems.Select(itm => itm.ToDto()).ToList()
            );
    }

    public static AddressDto ToDto(this Address adress)
    {
        return new AddressDto(
                FirstName: adress.FirstName,
                LastName: adress.LastName,
                EmailAddress: adress.EmailAddress!,
                AddressLine: adress.AddressLine,
                State: adress.State,
                ZipCode: adress.ZipCode,
                Country: adress.Country
                );
    }
    public static PaymentDto ToDto(this Payment payment) {
        return new PaymentDto(CardNumber: payment.CardNumber,
                              CardName: payment.CardName!,
                              ExpiresIn: payment.ExpiresIn,
                              Cvv: payment.CVV);
    }
    public static OrderItemDto ToDto(this OrderItem orderItem) {
        return new OrderItemDto(OrderId: orderItem.OrderId.Value,
                                ProductId: orderItem.ProductId.Value,
                                Quantity: orderItem.Quantity,
                                Price: orderItem.Price);
    }
    public static IEnumerable<OrderItemDto> ToDto(this IEnumerable<OrderItem> orderItemss) 
    {
        return orderItemss.Select(item=>item.ToDto());
    }
}
