namespace Eshop.Ordering.Application.Orders.Quaries.GetOrderByName;

public class GetOrderByNameHandler : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    private readonly IApplicationDbContext _dbContext;
    public GetOrderByNameHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .Where(a => a.OrderName.Value.Contains(request.Name))
                                        .AsNoTracking()
                                        .OrderBy(a => a.OrderName.Value)
                                        .ToListAsync(cancellationToken);
        if (orders == null || !orders.Any())
        {
            return new GetOrdersByNameResult(new List<OrderDto>());
        }
        // Convert the orders to OrderDto list
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }




}