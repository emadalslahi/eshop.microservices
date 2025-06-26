using Carter;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Application.Orders.Quaries.GetOrderByName;
using Mapster;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;

//public record GetOrdersByNameRequest(string Name);
public record   GetOrdersByNameResponse(IEnumerable<OrderDto> orders);
public class GetOrdersByName: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}",
            async (string name, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(name);
                var result = await sender.Send(query);
                var response = result.Adapt< GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .WithTags("Orders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
