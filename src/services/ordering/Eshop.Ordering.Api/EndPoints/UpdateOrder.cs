using Carter;
using Eshop.Ordering.Application.Dtos;
using Eshop.Ordering.Application.Orders.Commands.UpdateOrder;
using Mapster;
using MediatR;

namespace Eshop.Ordering.Api.EndPoints;


public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders",
            async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateOrder")
            .WithTags("Orders")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
} 