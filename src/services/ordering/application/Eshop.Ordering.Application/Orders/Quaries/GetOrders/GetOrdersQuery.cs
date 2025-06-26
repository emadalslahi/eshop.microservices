using eshop.buildingblocks.Pagination;

namespace Eshop.Ordering.Application.Orders.Quaries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginationResult<OrderDto> Orders);

