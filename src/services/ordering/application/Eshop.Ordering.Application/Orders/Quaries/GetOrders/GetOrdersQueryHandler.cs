using eshop.buildingblocks.Pagination;

namespace Eshop.Ordering.Application.Orders.Quaries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    private readonly IApplicationDbContext _dbContext;
    public GetOrdersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        List<Order> orders = await _dbContext
                            .Orders
                            .Include(e => e.OrderItems)
                            .AsNoTracking()
                            .OrderBy(o => o.OrderName.Value)
                            .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
                            .Take(request.PaginationRequest.PageSize)
                            .ToListAsync(cancellationToken);

        if (orders == null || !orders.Any())
        {
            return new GetOrdersResult(new PaginationResult<OrderDto>());
        }

        long totalCount = await _dbContext.Orders.LongCountAsync(cancellationToken);
        // Convert the orders to OrderDto list
        return new GetOrdersResult(Orders: new PaginationResult<OrderDto>(
                                                            pageIndex: request.PaginationRequest.PageIndex,
                                                            pageSize: request.PaginationRequest.PageSize,
                                                            count: totalCount,
                                                            data: orders.ToOrderDtoList()
                                                            ));
    }
}

