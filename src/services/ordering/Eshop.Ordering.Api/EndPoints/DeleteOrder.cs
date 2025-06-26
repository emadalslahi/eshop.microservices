using Carter;
using Eshop.Ordering.Application.Orders.Commands.DeleteOrder;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;

//public record DeleteOrderRequest(Guid Id);
public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new DeleteOrderCommand(id);
                var result = await sender.Send(command);
                var response = new DeleteOrderResponse(result.IsSuccess);
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .WithTags("Orders")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
} 