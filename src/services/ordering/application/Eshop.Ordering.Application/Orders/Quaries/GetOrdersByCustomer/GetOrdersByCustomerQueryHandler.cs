

namespace Eshop.Ordering.Application.Orders.Quaries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler(IApplicationDbContext _dbcontext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbcontext.Orders
            .Include(o => o.OrderItems)
            .Where(a => a.CustomerId == CustomerId.Of(request.id))
            .OrderBy(a => a.OrderName.Value)
            .ToListAsync(cancellationToken);

        if (orders == null || !orders.Any())
        {
            return new  GetOrdersByCustomerResult(new List<OrderDto>());
        }

        // Convert the orders to OrderDto list
        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
