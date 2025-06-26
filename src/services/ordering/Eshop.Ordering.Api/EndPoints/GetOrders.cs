using Carter;
using eshop.buildingblocks.Pagination;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Application.Orders.Quaries.GetOrders;
using Mapster;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;


public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginationResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",
            async (GetOrdersRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetOrdersQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt< GetOrdersResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .WithTags("Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
} 