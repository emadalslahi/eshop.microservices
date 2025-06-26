namespace Eshop.Ordering.Application.Orders.Quaries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid id) : IQuery<GetOrdersByCustomerResult>;
public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
