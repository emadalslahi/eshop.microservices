using Carter;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Application.Orders.Quaries.GetOrdersByCustomer;
using Mapster;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;

public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}",
            async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(customerId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrderByCustomerResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .WithTags("Orders")
            .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
} 